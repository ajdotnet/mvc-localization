using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace MyStocks.Mvc.Models
{
    [DebuggerDisplay("Models.Stock: {ID} {Isin} {Name} {Price} {Date}")]
    public class Stock
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localizations.Models), ErrorMessageResourceName = "Validation_Required")]
        [RegularExpression(@"[a-zA-Z]{2}[\w]{9}[\d]{1}", ErrorMessageResourceType = typeof(Localizations.Models), ErrorMessageResourceName = "Stock_Isin_RegEx")]
        [Display(ResourceType = typeof(Localizations.Models), Name = "Stock_Isin")]
        public string Isin { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localizations.Models), ErrorMessageResourceName = "Validation_Required")]
        [Display(ResourceType = typeof(Localizations.Models), Name = "Stock_Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localizations.Models), ErrorMessageResourceName = "Validation_Required")]
        [Display(ResourceType = typeof(Localizations.Models), Name = "Stock_Price")]
        [DisplayFormat(DataFormatString = "{0:f2}")] // currency would include currency sign
        public decimal Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localizations.Models), ErrorMessageResourceName = "Validation_Required")]
        [Display(ResourceType = typeof(Localizations.Models), Name = "Stock_Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}