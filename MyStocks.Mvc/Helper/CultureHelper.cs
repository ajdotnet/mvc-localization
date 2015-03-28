
using AJ.Common;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MyStocks.Mvc.Helper
{
    public static class CultureHelper
    {
        #region definitions

        const string CookieName = "PreferredCulture";

        [SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly")]
        public static readonly CultureInfo[] SupportedCultures = new CultureInfo[] 
        { 
            CultureInfo.GetCultureInfo("en-US"),
            CultureInfo.GetCultureInfo("de-DE"),
        };

        #endregion

        #region apply user culture

        public static void ApplyUserCulture(this HttpRequest request)
        {
            Guard.AssertNotNull(request, "request");

            ApplyUserCulture(request.Headers, request.Cookies);
        }

        static void ApplyUserCulture(NameValueCollection headers, HttpCookieCollection cookies)
        {
            var culture = GetPreferredCulture(cookies)
                ?? GetUserCulture(headers)
                ?? SupportedCultures[0];

            var t = Thread.CurrentThread;
            t.CurrentCulture = culture;
            t.CurrentUICulture = culture;
            Debug.WriteLine("Culture: " + culture.Name);
        }

        public static CultureInfo GetUserCulture(NameValueCollection headers)
        {
            Guard.AssertNotNull(headers, "headers");

            var acceptedCultures = GetUserCultures(headers["Accept-Language"]);
            var culture = GetMatchingCulture(acceptedCultures, SupportedCultures);
            return culture;
        }

        private static CultureInfo GetCultureInfo(string language)
        {
            try
            {
                return CultureInfo.GetCultureInfo(language);
            }
            catch (CultureNotFoundException)
            {
                return null;
            }
        }

        #endregion

        #region accept language header

        public static CultureInfo[] GetUserCultures(string acceptLanguage)
        {
            // Accept-Language: fr-FR , en;q=0.8 , en-us;q=0.5 , de;q=0.3
            if (string.IsNullOrWhiteSpace(acceptLanguage))
                return new CultureInfo[] { };

            var cultures = acceptLanguage
                .Split(',')
                .Select(s => WeightedLanguage.Parse(s))
                .OrderByDescending(w => w.Weight)
                .Select(w => GetCultureInfo(w.Language))
                .Where(ci => ci != null)
                .ToArray();
            return cultures;
        }

        class WeightedLanguage
        {
            public string Language { get; set; }
            public double Weight { get; set; }

            public static WeightedLanguage Parse(string weightedLanguageString)
            {
                // de
                // en;q=0.8
                var parts = weightedLanguageString.Split(';');
                var result = new WeightedLanguage { Language = parts[0].Trim(), Weight = 1.0 };
                if (parts.Length > 1)
                {
                    parts[1] = parts[1].Replace("q=", "").Trim();
                    double d;
                    if (double.TryParse(parts[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out d))
                        result.Weight = d;
                }
                return result;
            }
        }

        #endregion

        #region culture matching

        public static CultureInfo GetMatchingCulture(CultureInfo[] acceptedCultures, CultureInfo[] supportedCultures)
        {
            return
                // first pass: exact matches as well as requested neutral matching supported region
                //      supported: en-US, de-DE
                //      requested: de, en-US;q=0.8  
                //                  => de-DE!  (de has precendence over en-US)
                GetMatch(acceptedCultures, supportedCultures, MatchesCompletely)
                // second pass: look for requested neutral matching supported _neutral_ region
                //      supported: en-US, de-DE
                //      requested: de-AT, en-GB;q=0.8  
                //                  => de-DE! (no exact match, but de-AT has better fit than en-GB)
                ?? GetMatch(acceptedCultures, supportedCultures, MatchesPartly);
        }

        public static CultureInfo GetMatch(CultureInfo[] acceptedCultures, CultureInfo[] supportedCultures, Func<CultureInfo, CultureInfo, bool> predicate)
        {
            Guard.AssertNotNull(acceptedCultures, "acceptedCultures");
            Guard.AssertNotNull(supportedCultures, "supportedCultures");

            foreach (var acceptedCulture in acceptedCultures)
            {
                var match = supportedCultures
                    .Where(supportedCulture => predicate(acceptedCulture, supportedCulture))
                    .FirstOrDefault();
                if (match != null)
                    return match;
            }
            return null;
        }

        static bool MatchesCompletely(CultureInfo acceptedCulture, CultureInfo supportedCulture)
        {
            if (supportedCulture.Name == acceptedCulture.Name)
                return true;
            // acceptedCulture could be neutral and supportedCulture specific, but this is still a match (de matches de-DE, de-AT, ...)
            if (acceptedCulture.IsNeutralCulture)
            {
                if (supportedCulture.Parent.Name == acceptedCulture.Name)
                    return true;
            }
            return false;
        }

        static bool MatchesPartly(CultureInfo acceptedCulture, CultureInfo supportedCulture)
        {
            supportedCulture = supportedCulture.Parent;
            if (!acceptedCulture.IsNeutralCulture)
                acceptedCulture = acceptedCulture.Parent;

            if (supportedCulture.Name == acceptedCulture.Name)
                return true;
            return false;
        }

        #endregion

        #region remember custom choice

        public static void SetPreferredCulture(this HttpResponseBase response, string cultureName)
        {
            Guard.AssertNotNull(response, "response");

            SetPreferredCulture(response.Cookies, cultureName);
        }

        static void SetPreferredCulture(HttpCookieCollection cookies, string cultureName)
        {
            var cookie = new HttpCookie(CookieName, cultureName);
            cookie.Expires = DateTime.Now.AddDays(30);
            cookies.Set(cookie);
            Debug.WriteLine("SetPreferredCulture: " + cultureName);
        }

        static CultureInfo GetPreferredCulture(HttpCookieCollection cookies)
        {
            var cookie = cookies[CookieName];
            if (cookie == null)
                return null;
            var culture = GetCultureInfo((string)cookie.Value);
            if (culture == null)
                return null;
            if (!SupportedCultures.Where(ci => ci.Name == culture.Name).Any())
                return null;
            return culture;
        }

        #endregion

        #region cycle supported cultures (ui support)

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "0#")]
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
        public static void GetSwitchCultures(out CultureInfo currentCulture, out CultureInfo nextCulture)
        {
            currentCulture = Thread.CurrentThread.CurrentUICulture;
            var currentIndex = Array.IndexOf(SupportedCultures.Select(ci => ci.Name).ToArray(), currentCulture.Name);
            int nextIndex = (currentIndex + 1) % SupportedCultures.Length;
            nextCulture = SupportedCultures[nextIndex];
        }

        #endregion

        #region initialization

        public static void InitializeLocalization()
        {
            //DefaultModelBinder.ResourceClassKey = "GlobalResources";
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "GlobalResources";
        }

        #endregion
    }
}
