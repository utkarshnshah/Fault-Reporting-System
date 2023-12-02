using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FaultReportingSystem.Data;
using FaultReportingSystem.Models;

namespace FaultReportingSystem
{
    public class ManagersController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ManagersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Managers != null ? 
                          View(await _context.Managers.ToListAsync()) :
                          Problem("Manager 'ApplicationDbContext.Managers'  not found.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }
            var manager = await _context.Managers
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagerId,ManagerFirstName,ManagerLastName,ManagerEmail,ManagerPassword,ManagerContactNumber")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manager);
                await _context.SaveChangesAsync();
                TempData["ManagerModificationMessage"] = "A new Manager has been added!";
                return RedirectToAction(nameof(Index));
            }
            return View(manager);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManagerId,ManagerFirstName,ManagerLastName,ManagerEmail,ManagerPassword,ManagerContactNumber")] Manager manager)
        {
            if (id != manager.ManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                    TempData["ManagerModificationMessage"] = "Manager details has been updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.ManagerId))
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
            return View(manager);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Managers == null)
            {
                return Problem("Manager 'ApplicationDbContext.Managers' not found.");
            }
            var manager = await _context.Managers.FindAsync(id);
            if (manager != null)
            {
                _context.Managers.Remove(manager);
                TempData["ManagerModificationMessage"] = "Selected Manager has been removed!";
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
          return (_context.Managers?.Any(e => e.ManagerId == id)).GetValueOrDefault();
        }
    }
}