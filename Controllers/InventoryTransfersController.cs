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
    public class InventoryTransfersController : Controller
    {
        private readonly InventoryApplicationContext _context;

        public InventoryTransfersController(InventoryApplicationContext context)
        {
            _context = context;
        }

        // GET: InventoryTransfers
        public async Task<IActionResult> Index()
        {
            var inventoryApplicationContext = _context.InventoryTransfer
                .Include(i => i.Department)
                .Include(i => i.DepartmentResponsibleEmployee)
                .Include(i => i.Operation)
                .Include(i => i.Signatory);

            return View(await inventoryApplicationContext.ToListAsync());
        }

        // GET: InventoryTransfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryTransfer = await _context.InventoryTransfer
                .Include(i => i.Inventories)
                .ThenInclude(i => i.Inventory)
                .Include(i => i.Department)
                .Include(i => i.DepartmentResponsibleEmployee)
                .Include(i => i.Operation)
                .Include(i => i.Signatory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (inventoryTransfer == null)
            {
                return NotFound();
            }

            return View(inventoryTransfer);
        }

        // GET: InventoryTransfers/Create
        public IActionResult Create()
        {
            ViewData["InventoryId"] = new SelectList(_context.Inventory, "Id", "FullName");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name");
            ViewData["DepartmentResponsibleEmployeeId"] = new SelectList(_context.Employee, "Id", "FullName");
            ViewData["OperationId"] = new SelectList(_context.Operation, "Id", "Name");
            ViewData["SignatoryId"] = new SelectList(_context.Employee, "Id", "FullName");
            return View();
        }

        // POST: InventoryTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Inventories,OperationId,SignatoryId,DepartmentId,DepartmentResponsibleEmployeeId")] InventoryTransfer inventoryTransfer)
        {
            if (ModelState.IsValid)
            {
                string[] inventoriesIds = ModelState["Inventories"].AttemptedValue.Split(",");
                foreach (var inventoryId in inventoriesIds)
                {
                    inventoryTransfer.Inventories.Add(new InventoryTransferList { InventoryId = Convert.ToInt32(inventoryId) });
                }
                _context.Add(inventoryTransfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventory, "Id", "FullName");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", inventoryTransfer.DepartmentId);
            ViewData["DepartmentResponsibleEmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", inventoryTransfer.DepartmentResponsibleEmployeeId);
            ViewData["OperationId"] = new SelectList(_context.Operation, "Id", "Name", inventoryTransfer.OperationId);
            ViewData["SignatoryId"] = new SelectList(_context.Employee, "Id", "FullName", inventoryTransfer.SignatoryId);
            return View(inventoryTransfer);
        }

        // GET: InventoryTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryTransfer = await _context.InventoryTransfer.FindAsync(id);
            if (inventoryTransfer == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", inventoryTransfer.DepartmentId);
            ViewData["DepartmentResponsibleEmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", inventoryTransfer.DepartmentResponsibleEmployeeId);
            ViewData["OperationId"] = new SelectList(_context.Operation, "Id", "Name", inventoryTransfer.OperationId);
            ViewData["SignatoryId"] = new SelectList(_context.Employee, "Id", "FullName", inventoryTransfer.SignatoryId);
            return View(inventoryTransfer);
        }

        // POST: InventoryTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Inventories,OperationId,SignatoryId,DepartmentId,DepartmentResponsibleEmployeeId")] InventoryTransfer inventoryTransfer)
        {
            if (id != inventoryTransfer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryTransferExists(inventoryTransfer.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", inventoryTransfer.DepartmentId);
            ViewData["DepartmentResponsibleEmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", inventoryTransfer.DepartmentResponsibleEmployeeId);
            ViewData["OperationId"] = new SelectList(_context.Operation, "Id", "Name", inventoryTransfer.OperationId);
            ViewData["SignatoryId"] = new SelectList(_context.Employee, "Id", "FullName", inventoryTransfer.SignatoryId);
            return View(inventoryTransfer);
        }

        // GET: InventoryTransfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryTransfer = await _context.InventoryTransfer
                .Include(i => i.Inventories)
                .Include(i => i.Department)
                .Include(i => i.DepartmentResponsibleEmployee)
                .Include(i => i.Operation)
                .Include(i => i.Signatory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryTransfer == null)
            {
                return NotFound();
            }

            return View(inventoryTransfer);
        }

        // POST: InventoryTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryTransfer = await _context.InventoryTransfer.FindAsync(id);
            if (inventoryTransfer != null)
            {
                _context.InventoryTransfer.Remove(inventoryTransfer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryTransferExists(int id)
        {
            return _context.InventoryTransfer.Any(e => e.Id == id);
        }
    }
}
