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
    public class DevelopersController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public DevelopersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            PopulateDropdowns();
            var applicationDbContext = _context.Developers.Include(d => d.Manager);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            PopulateDropdowns();
            if (id == null || _context.Developers == null)
            {
                return NotFound();
            }
            var developer = await _context.Developers
                .Include(d => d.Manager)
                .FirstOrDefaultAsync(m => m.DeveloperId == id);
            if (developer == null)
            {
                return NotFound();
            }
            return View(developer);
        }

        public IActionResult Create()
        {
            PopulateDropdowns();
            ViewData["ManagerId"] = new SelectList(_context.Managers, "ManagerId", "ManagerId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeveloperId,DeveloperFirstName,DeveloperLastName,DeveloperEmail,DeveloperPassword,DeveloperContactNumber,ManagerId")] Developer developer)
        {
            PopulateDropdowns();
            if (ModelState.IsValid)
            {
                _context.Add(developer);
                await _context.SaveChangesAsync();
                TempData["DeveloperModificationMessage"] = "A new Developer has been added!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Managers, "ManagerId", "ManagerId", developer.ManagerId);
            return View(developer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            PopulateDropdowns();
            if (id == null || _context.Developers == null)
            {
                return NotFound();
            }

            var developer = await _context.Developers.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }
            ViewData["ManagerId"] = new SelectList(_context.Managers, "ManagerId", "ManagerId", developer.ManagerId);
            return View(developer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeveloperId,DeveloperFirstName,DeveloperLastName,DeveloperEmail,DeveloperPassword,DeveloperContactNumber,ManagerId")] Developer developer)
        {
            PopulateDropdowns();
            if (id != developer.DeveloperId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(developer);
                    await _context.SaveChangesAsync();
                    TempData["DeveloperModificationMessage"] = "Developer details has been updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeveloperExists(developer.DeveloperId))
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
            ViewData["ManagerId"] = new SelectList(_context.Managers, "ManagerId", "ManagerId", developer.ManagerId);
            return View(developer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            PopulateDropdowns();
            if (id == null || _context.Developers == null)
            {
                return NotFound();
            }

            var developer = await _context.Developers
                .Include(d => d.Manager)
                .FirstOrDefaultAsync(m => m.DeveloperId == id);
            if (developer == null)
            {
                return NotFound();
            }

            return View(developer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PopulateDropdowns();
            if (_context.Developers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Developers'  is null.");
            }
            var developer = await _context.Developers.FindAsync(id);
            if (developer != null)
            {
                _context.Developers.Remove(developer);
                TempData["DeveloperModificationMessage"] = "Selected Developer has been deleted!";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeveloperExists(int id)
        {
          return (_context.Developers?.Any(e => e.DeveloperId == id)).GetValueOrDefault();
        }
        private void PopulateDropdowns()
        {
            var developers = _context.Developers.Select(d => new
            {
                d.DeveloperId,
                DeveloperFullNameWithEmail = $"{d.DeveloperFirstName} {d.DeveloperLastName} ({d.DeveloperEmail})"
            }).ToList();
            var managers = _context.Managers.Select(m => new
            {
                m.ManagerId,
                ManagerFullNameWithEmail = $"{m.ManagerFirstName} {m.ManagerLastName} ({m.ManagerEmail})"
            }).ToList();

            ViewBag.Developers = new SelectList(developers, "DeveloperId", "DeveloperFullNameWithEmail");
            ViewBag.Managers = new SelectList(managers, "ManagerId", "ManagerFullNameWithEmail");
        }
    }
}
