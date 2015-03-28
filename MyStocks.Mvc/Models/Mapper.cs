
using ContractStock = MyStocks.BusinessContract.Stock; // business contract model
using ModelStock = MyStocks.Mvc.Models.Stock; // mvc model

namespace MyStocks.Mvc.Models
{
    static class Mapper
    {
        public static ModelStock MapToUI(this ContractStock stock)
        {
            return new ModelStock
            {
                Date = stock.Date,
                ID = stock.ID,
                Isin = stock.Isin,
                Name = stock.Name,
                Price = stock.Price
            };
        }

        public static ContractStock MapToContract(this ModelStock stock)
        {
            return new ContractStock
            {
                Date = stock.Date,
                ID = stock.ID,
                Isin = stock.Isin,
                Name = stock.Name,
                Price = stock.Price
            };
        }
    }
}