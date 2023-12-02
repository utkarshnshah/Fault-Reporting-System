using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FaultReportingSystem.Data;
using FaultReportingSystem.Models;

namespace FaultReportingSystem.Controllers
{
    public class HelpDesksController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public HelpDesksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.HelpDesks != null ? 
                          View(await _context.HelpDesks.ToListAsync()) :
                          Problem("HelpDesk 'ApplicationDbContext.HelpDesks'  not found.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HelpDesks == null)
            {
                return NotFound();
            }

            var helpDesk = await _context.HelpDesks
                .FirstOrDefaultAsync(m => m.HelpDeskId == id);
            if (helpDesk == null)
            {
                return NotFound();
            }

            return View(helpDesk);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HelpDeskId,HelpDeskFirstName,HelpDeskLastName,HelpDeskEmail,HelpDeskPassword,HelpDeskContactNumber")] HelpDesk helpDesk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(helpDesk);
                await _context.SaveChangesAsync();
                TempData["HelpDeskModificationMessage"] = "A new Help Desk staff has been added!";
                return RedirectToAction(nameof(Index));
            }
            return View(helpDesk);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HelpDesks == null)
            {
                return NotFound();
            }

            var helpDesk = await _context.HelpDesks.FindAsync(id);
            if (helpDesk == null)
            {
                return NotFound();
            }
            return View(helpDesk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HelpDeskId,HelpDeskFirstName,HelpDeskLastName,HelpDeskEmail,HelpDeskPassword,HelpDeskContactNumber")] HelpDesk helpDesk)
        {
            if (id != helpDesk.HelpDeskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helpDesk);
                    await _context.SaveChangesAsync();
                    TempData["HelpDeskModificationMessage"] = "Help Desk Staff details have been updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelpDeskExists(helpDesk.HelpDeskId))
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
            return View(helpDesk);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HelpDesks == null)
            {
                return NotFound();
            }

            var helpDesk = await _context.HelpDesks
                .FirstOrDefaultAsync(m => m.HelpDeskId == id);
            if (helpDesk == null)
            {
                return NotFound();
            }

            return View(helpDesk);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HelpDesks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HelpDesks'  is null.");
            }
            var helpDesk = await _context.HelpDesks.FindAsync(id);
            if (helpDesk != null)
            {
                _context.HelpDesks.Remove(helpDesk);
                TempData["HelpDeskModificationMessage"] = "Selected Help Desk Staff have been removed!";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelpDeskExists(int id)
        {
          return (_context.HelpDesks?.Any(e => e.HelpDeskId == id)).GetValueOrDefault();
        }
    }
}
