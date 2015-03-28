using MyStocks.Mvc.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;

namespace MyStocks.Mvc.Controllers
{
    public class StockController : Controller
    {
        protected MyStocks.BusinessContract.IStockService StockService { get; set; }

        public StockController()
        {
            this.StockService = new MyStocks.BusinessService.StockService();
        }

        //
        // GET: /Stock/
        public ActionResult Index()
        {
            var model = StockService.GetAllStocks().Select(Mapper.MapToUI);
            return View(model);
        }

        //
        // GET: /Stock/Details/5
        public ActionResult Details(int id)
        {
            var model = StockService.GetStock(id).MapToUI();
            return View(model);
        }

        //
        // GET: /Stock/Edit/5
        public ActionResult Edit(int id)
        {
            var model = StockService.GetStock(id).MapToUI();
            return View(model);
        }

        //
        // POST: /Stock/Edit/5
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [HttpPost]
        public ActionResult Edit(Stock stock)
        {
            if (stock == null)
                return RedirectToAction("Index");

            try
            {
                StockService.UpdateStock(stock.MapToContract());
                return RedirectToAction("Details", new { id = stock.ID });
            }
            catch (Exception)
            {
                var model = StockService.GetStock(stock.ID).MapToUI();
                return View(model);
            }
        }

    }
}
