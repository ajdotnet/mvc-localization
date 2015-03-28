using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MyStocks.BusinessContract
{
    [DebuggerDisplay("BusinessContract.Stock: {ID} {Isin} {Name} {Price} {Date}")]
    public class Stock
    {
        public int ID { get; set; }
        /// <summary>
        /// http://en.wikipedia.org/wiki/International_Securities_Identification_Number
        /// 12-character alpha-numerical code 
        /// </summary>
        public string Isin { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
