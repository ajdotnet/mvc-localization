using MyStocks.BusinessContract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStocks.BusinessService
{
    public class StockService : IStockService
    {
        public Stock[] GetAllStocks()
        {
            return GetTestData().OrderBy(s => s.Name).ToArray();
        }

        public Stock GetStock(int id)
        {
            return GetTestData().Where(s => s.ID == id).First();
        }

        public void UpdateStock(Stock stock)
        {
            // nop...
        }

        static IEnumerable<Stock> GetTestData()
        {
            int id = 0;
            yield return new Stock { ID = ++id, Isin = "DE000CBK1001", Name = "Commerzbank AG ", Price = 9.75m, Date = DateTime.Today };
            yield return new Stock { ID = ++id, Isin = "DE0005557508", Name = "Deutsche Telekom AG ", Price = 11.93m, Date = DateTime.Today };
            yield return new Stock { ID = ++id, Isin = "DE000BAY0017", Name = "Bayer AG", Price = 89.47m, Date = DateTime.Today };
            yield return new Stock { ID = ++id, Isin = "DE000BASF111", Name = "BASF", Price = 73.11m, Date = DateTime.Today };
            yield return new Stock { ID = ++id, Isin = "DE0006231004", Name = "Infineon Technologies AG ", Price = 7.24m, Date = DateTime.Today };
            yield return new Stock { ID = ++id, Isin = "DE0008232125", Name = "Deutsche Lufthansa AG", Price = 14.05m, Date = DateTime.Today };
            yield return new Stock { ID = ++id, Isin = "DE0005140008", Name = "Deutsche Bank AG", Price = 36.55m, Date = DateTime.Today };
            yield return new Stock { ID = ++id, Isin = "DE000A1EWWW0", Name = "Adidas AG", Price = 81.76m, Date = DateTime.Today };
        }
    }
}
