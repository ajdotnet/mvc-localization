
namespace MyStocks.BusinessContract
{
    public interface IStockService
    {
        Stock[] GetAllStocks();
        Stock GetStock(int id);
        void UpdateStock(Stock stock);
    }
}
