using InventoryApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTakingApplication.Models;
using System.Diagnostics;

namespace StockTakingApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InventoryApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, InventoryApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string search = "")
        {
            if (_context.Inventory == null)
            {
                return Problem("Entity set 'InventoryApplication.Inventory'  is null.");
            }
            if (_context.InventoryReceipt == null)
            {
                return Problem("Entity set 'InventoryApplication.InventoryReceipt'  is null.");
            }
            if (_context.InventoryTransfer == null)
            {
                return Problem("Entity set 'InventoryApplication.InventoryTransfer'  is null.");
            }

            var inventory = from i in _context.Inventory
                            select i;

            var operation = from o in _context.Operation
                            select o;

            var inventoryReceipt = from ir in _context.InventoryReceipt
                                   select ir;

            var inventoryTransfer = from it in _context.InventoryTransfer
                                    join o in operation on it.OperationId equals o.Id
                                    select it;

            if (!string.IsNullOrEmpty(search))
            {
                inventory = from i in inventory
                            where i.Name.Contains(search) || i.SerialNumber!.Contains(search)
                            select i;

                inventoryReceipt = from ir in inventoryReceipt
                                   join i in inventory on ir.InventoryId equals i.Id
                                   select ir;

                inventoryTransfer = from it in inventoryTransfer
                                    join i in inventory on it.Inventories[0].InventoryId equals i.Id
                                    select it;
            }

            ViewData["search"] = search;
            ViewData["Inventory"] = inventory;
            ViewData["InventoryReceipt"] = inventoryReceipt.Include(ir => ir.Supplier).Include(ir => ir.Inventory);
            ViewData["InventoryTransfer"] = inventoryTransfer.Include(it => it.Operation);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
