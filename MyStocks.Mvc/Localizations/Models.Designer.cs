﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyStocks.Mvc.Localizations {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Models {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Models() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MyStocks.Mvc.Localizations.Models", typeof(Models).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date.
        /// </summary>
        public static string Stock_Date {
            get {
                return ResourceManager.GetString("Stock_Date", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ISIN.
        /// </summary>
        public static string Stock_Isin {
            get {
                return ResourceManager.GetString("Stock_Isin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field ISIN must start with 2 letters, followed by 9 alphanumeric symbols, followed by a digit..
        /// </summary>
        public static string Stock_Isin_RegEx {
            get {
                return ResourceManager.GetString("Stock_Isin_RegEx", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name.
        /// </summary>
        public static string Stock_Name {
            get {
                return ResourceManager.GetString("Stock_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Price.
        /// </summary>
        public static string Stock_Price {
            get {
                return ResourceManager.GetString("Stock_Price", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is a required field!.
        /// </summary>
        public static string Validation_Required {
            get {
                return ResourceManager.GetString("Validation_Required", resourceCulture);
            }
        }
    }
}
