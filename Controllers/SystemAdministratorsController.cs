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
    public class SystemAdministratorsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public SystemAdministratorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.SystemAdministrators != null ? 
                          View(await _context.SystemAdministrators.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SystemAdministrators'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SystemAdministrators == null)
            {
                return NotFound();
            }

            var systemAdministrator = await _context.SystemAdministrators
                .FirstOrDefaultAsync(m => m.SystemAdministratorId == id);
            if (systemAdministrator == null)
            {
                return NotFound();
            }

            return View(systemAdministrator);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SystemAdministratorId,FirstName,LastName,Email,Password,ContactNumber")] SystemAdministrator systemAdministrator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemAdministrator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemAdministrator);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SystemAdministrators == null)
            {
                return NotFound();
            }

            var systemAdministrator = await _context.SystemAdministrators.FindAsync(id);
            if (systemAdministrator == null)
            {
                return NotFound();
            }
            return View(systemAdministrator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SystemAdministratorId,FirstName,LastName,Email,Password,ContactNumber")] SystemAdministrator systemAdministrator)
        {
            if (id != systemAdministrator.SystemAdministratorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemAdministrator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemAdministratorExists(systemAdministrator.SystemAdministratorId))
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
            return View(systemAdministrator);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SystemAdministrators == null)
            {
                return NotFound();
            }

            var systemAdministrator = await _context.SystemAdministrators
                .FirstOrDefaultAsync(m => m.SystemAdministratorId == id);
            if (systemAdministrator == null)
            {
                return NotFound();
            }

            return View(systemAdministrator);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemAdministrators == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SystemAdministrators'  is null.");
            }
            var systemAdministrator = await _context.SystemAdministrators.FindAsync(id);
            if (systemAdministrator != null)
            {
                _context.SystemAdministrators.Remove(systemAdministrator);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemAdministratorExists(int id)
        {
          return (_context.SystemAdministrators?.Any(e => e.SystemAdministratorId == id)).GetValueOrDefault();
        }
    }
}
