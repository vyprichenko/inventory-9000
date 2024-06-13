using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryApplication.Data;
using InventoryApplication.Models;

namespace InventoryApplication.Controllers
{
    public class InventoryReceiptsController : Controller
    {
        private readonly InventoryApplicationContext _context;

        public InventoryReceiptsController(InventoryApplicationContext context)
        {
            _context = context;
        }

        // GET: InventoryReceipts
        public async Task<IActionResult> Index()
        {
            var inventoryApplicationContext = _context.InventoryReceipt.Include(i => i.Inventory).Include(i => i.Supplier);
            return View(await inventoryApplicationContext.ToListAsync());
        }

        // GET: InventoryReceipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryReceipt = await _context.InventoryReceipt
                .Include(i => i.Inventory)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryReceipt == null)
            {
                return NotFound();
            }

            return View(inventoryReceipt);
        }

        // GET: InventoryReceipts/Create
        public IActionResult Create()
        {
            ViewData["InventoryId"] = new SelectList(_context.Set<Inventory>(), "Id", "FullName");
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name");
            return View();
        }

        // POST: InventoryReceipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Price,Date,SupplierId,InventoryId")] InventoryReceipt inventoryReceipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryId"] = new SelectList(_context.Set<Inventory>(), "Id", "FullName", inventoryReceipt.InventoryId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", inventoryReceipt.SupplierId);
            return View(inventoryReceipt);
        }

        // GET: InventoryReceipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryReceipt = await _context.InventoryReceipt.FindAsync(id);
            if (inventoryReceipt == null)
            {
                return NotFound();
            }
            ViewData["InventoryId"] = new SelectList(_context.Set<Inventory>(), "Id", "FullName", inventoryReceipt.InventoryId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", inventoryReceipt.SupplierId);
            return View(inventoryReceipt);
        }

        // POST: InventoryReceipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Date,SupplierId,InventoryId")] InventoryReceipt inventoryReceipt)
        {
            if (id != inventoryReceipt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryReceiptExists(inventoryReceipt.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryId"] = new SelectList(_context.Set<Inventory>(), "Id", "FullName", inventoryReceipt.InventoryId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", inventoryReceipt.SupplierId);
            return View(inventoryReceipt);
        }

        // GET: InventoryReceipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryReceipt = await _context.InventoryReceipt
                .Include(i => i.Inventory)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryReceipt == null)
            {
                return NotFound();
            }

            return View(inventoryReceipt);
        }

        // POST: InventoryReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryReceipt = await _context.InventoryReceipt.FindAsync(id);
            if (inventoryReceipt != null)
            {
                _context.InventoryReceipt.Remove(inventoryReceipt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryReceiptExists(int id)
        {
            return _context.InventoryReceipt.Any(e => e.Id == id);
        }
    }
}
