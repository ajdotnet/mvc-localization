using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyStocks.Mvc.Helper;
using System.Globalization;

namespace MyStocks.UnitTests
{
    [TestClass]
    public class TestCultureHelper
    {

        [TestMethod]
        public void TestPerfectMatch()
        {
            //1. If the user's request is "de-DE, en-US;q=0.8", then "de-DE" is the perfect match.
            var acceptLanguage = "de-DE, en-US;q=0.8";
            CultureInfo[] acceptedCultures = CultureHelper.GetUserCultures(acceptLanguage);
            CultureInfo[] supportedCultures = CultureHelper.SupportedCultures;
            var expected = "de-DE";
            var actual = CultureHelper.GetMatchingCulture(acceptedCultures, supportedCultures);
            Assert.AreEqual(expected, actual.Name);
        }

        [TestMethod]
        public void TestNeutralMatch()
        {
            //2. If his request is "de, en-US;q=0.8", then "de-DE" is still the correct choice, because "de" encompasses all german regions.
            var acceptLanguage = "de, en-US;q=0.8";
            CultureInfo[] acceptedCultures = CultureHelper.GetUserCultures(acceptLanguage);
            CultureInfo[] supportedCultures = CultureHelper.SupportedCultures;
            var expected = "de-DE";
            var actual = CultureHelper.GetMatchingCulture(acceptedCultures, supportedCultures);
            Assert.AreEqual(expected, actual.Name);
        }

        [TestMethod]
        public void TestPerfectMatch2()
        {
            //3. If the user's request is "fr-FR, en-US;q=0.8", then "en-US" is the perfect match. Not the users' first choice, but anyway.
            var acceptLanguage = "fr-FR, en-US;q=0.8";
            CultureInfo[] acceptedCultures = CultureHelper.GetUserCultures(acceptLanguage);
            CultureInfo[] supportedCultures = CultureHelper.SupportedCultures;
            var expected = "en-US";
            var actual = CultureHelper.GetMatchingCulture(acceptedCultures, supportedCultures);
            Assert.AreEqual(expected, actual.Name);
        }

        [TestMethod]
        public void TestPerfectMatchWithNeutral()
        {
            //4. If his request is "de-AT, en-US;q=0.8", then "en-US" is still the best choice, because it's a perfect match, while de-AT matches de-DE only partly. 
            //   This is debatable, but since regions also include number and date formats, etc., de-AT could result in more issues for the user 
            //   (and he could have asked for "de" if he wanted another outcome).
            var acceptLanguage = "de-AT, en-US;q=0.8";
            CultureInfo[] acceptedCultures = CultureHelper.GetUserCultures(acceptLanguage);
            CultureInfo[] supportedCultures = CultureHelper.SupportedCultures;
            var expected = "en-US";
            var actual = CultureHelper.GetMatchingCulture(acceptedCultures, supportedCultures);
            Assert.AreEqual(expected, actual.Name);
        }

        [TestMethod]
        public void TestSecondaryMatch()
        {
            //5. If his request is "de-AT, fr-GB;q=0.8", then we have no perfect match. But "de-DE" matches "de-AT" at least partly, so it would be a better choice, 
            //   than using the fallback to the default region en-US.
            var acceptLanguage = "de-AT, en-GB;q=0.8";
            CultureInfo[] acceptedCultures = CultureHelper.GetUserCultures(acceptLanguage);
            CultureInfo[] supportedCultures = CultureHelper.SupportedCultures;
            var expected = "de-DE";
            var actual = CultureHelper.GetMatchingCulture(acceptedCultures, supportedCultures);
            Assert.AreEqual(expected, actual.Name);
        }

        [TestMethod]
        public void TestNoMatch()
        {
            //6. If no requested region matches any of the supported cultures even slightly, e.g. "es-ES, fr-FR;q=0.8", the default region e-US is used - nothing else that could be done.
            var acceptLanguage = "es-ES, fr-FR;q=0.8";
            CultureInfo[] acceptedCultures = CultureHelper.GetUserCultures(acceptLanguage);
            CultureInfo[] supportedCultures = CultureHelper.SupportedCultures;
            string expected = null;
            var actual = CultureHelper.GetMatchingCulture(acceptedCultures, supportedCultures);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAbuse()
        {
            //6. If no requested region matches any of the supported cultures even slightly, e.g. "es-ES, fr-FR;q=0.8", the default region e-US is used - nothing else that could be done.
            CultureInfo[] supportedCultures = CultureHelper.SupportedCultures;

            Assert.IsNull(CultureHelper.GetMatchingCulture(CultureHelper.GetUserCultures("de-DE-dddd, fr-FR;q=0.8"), supportedCultures));
            Assert.IsNull(CultureHelper.GetMatchingCulture(CultureHelper.GetUserCultures("x-klingon, fr-FR;q=0.8"), supportedCultures));
            Assert.IsNull(CultureHelper.GetMatchingCulture(CultureHelper.GetUserCultures(null), supportedCultures));

            Assert.AreEqual("de-DE", CultureHelper.GetMatchingCulture(CultureHelper.GetUserCultures("de-DE;xxx"), supportedCultures).Name);
            Assert.AreEqual("de-DE", CultureHelper.GetMatchingCulture(CultureHelper.GetUserCultures("de-DE;q=xxx"), supportedCultures).Name);
            Assert.AreEqual("de-DE", CultureHelper.GetMatchingCulture(CultureHelper.GetUserCultures("de-DE;q=0.8;q=0.5"), supportedCultures).Name);

            Assert.IsNull(CultureHelper.GetMatchingCulture(CultureHelper.GetUserCultures(",,,,,"), supportedCultures));
            Assert.IsNull(CultureHelper.GetMatchingCulture(CultureHelper.GetUserCultures(";;;;"), supportedCultures));
        }
    }
}
