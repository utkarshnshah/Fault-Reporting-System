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
    public class FaultLogsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public FaultLogsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult ViewFaultLogs(int faultId)
        {
            PopulateDropdowns();
            var faultWithLogs = _context.Faults.Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Include(f => f.FaultLogs)
                .FirstOrDefault(f => f.FaultId == faultId);
            if (faultWithLogs == null)
            {
                return NotFound();
            }

            return View("ViewFaultLogs", faultWithLogs);
        }

        public async Task<IActionResult> Index()
        {
            PopulateDropdowns();
            var applicationDbContext = _context.FaultLogs.Include(f => f.Developer).Include(f => f.Fault);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            PopulateDropdowns();
            if (id == null || _context.FaultLogs == null)
            {
                return NotFound();
            }

            var faultLog = await _context.FaultLogs
                .Include(f => f.Developer)
                .Include(f => f.Fault)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (faultLog == null)
            {
                return NotFound();
            }

            return View(faultLog);
        }

        public IActionResult Create(int faultId, int developerId)
        {
            PopulateDropdowns();
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId");
            ViewData["FaultId"] = new SelectList(_context.Faults, "FaultId", "FaultId");
            ViewData["SelectedFaultId"] = faultId;
            ViewData["SelectedDeveloperId"] = developerId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogId,DateTime,Action,DeveloperId,FaultId")] FaultLog faultLog)
        {
            PopulateDropdowns();
            if (ModelState.IsValid)
            {
                _context.Add(faultLog);
                await _context.SaveChangesAsync();
                TempData["FaultLogAddedMessage"] = "New Fault Log Added!";
                return RedirectToAction("ViewFaultLogs", new { faultId = faultLog.FaultId });
            }
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId", faultLog.DeveloperId);
            ViewData["FaultId"] = new SelectList(_context.Faults, "FaultId", "FaultId", faultLog.FaultId);
            return View(faultLog);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            PopulateDropdowns();
            if (id == null || _context.FaultLogs == null)
            {
                return NotFound();
            }

            var faultLog = await _context.FaultLogs.FindAsync(id);
            if (faultLog == null)
            {
                return NotFound();
            }
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId", faultLog.DeveloperId);
            ViewData["FaultId"] = new SelectList(_context.Faults, "FaultId", "FaultId", faultLog.FaultId);
            return View(faultLog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogId,DateTime,Action,DeveloperId,FaultId")] FaultLog faultLog)
        {
            PopulateDropdowns();
            if (id != faultLog.LogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                PopulateDropdowns();
                try
                {
                    _context.Update(faultLog);
                    await _context.SaveChangesAsync();
                    TempData["FaultLogUpdatedMessage"] = "Fault Log Updated Successfully!";                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultLogExists(faultLog.LogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ViewFaultLogs", new { faultId = faultLog.FaultId });
            }
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId", faultLog.DeveloperId);
            ViewData["FaultId"] = new SelectList(_context.Faults, "FaultId", "FaultId", faultLog.FaultId);
            return View(faultLog);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            PopulateDropdowns();
            if (id == null || _context.FaultLogs == null)
            {
                return NotFound();
            }

            var faultLog = await _context.FaultLogs
                .Include(f => f.Developer)
                .Include(f => f.Fault)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (faultLog == null)
            {
                return NotFound();
            }

            return View(faultLog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PopulateDropdowns();
            if (_context.FaultLogs == null)
            {
                return Problem("Fault Log 'ApplicationDbContext.FaultLogs' not found.");
            }
            var faultLog = await _context.FaultLogs.FindAsync(id);
            if (faultLog != null)
            {
                _context.FaultLogs.Remove(faultLog);
            }            
            await _context.SaveChangesAsync();
            TempData["FaultLogDeletedMessage"] = "Fault Log Deleted Successfully!";
            return RedirectToAction("ViewFaultLogs", new { faultId = faultLog.FaultId });
        }

        private bool FaultLogExists(int id)
        {
          return (_context.FaultLogs?.Any(e => e.LogId == id)).GetValueOrDefault();
        }

        private void PopulateDropdowns()
        {
            var developers = _context.Developers.Select(d => new
            {
                d.DeveloperId,
                DeveloperFullNameWithEmail = $"{d.DeveloperFirstName} {d.DeveloperLastName} ({d.DeveloperEmail})"
            }).ToList();
            var customers = _context.Customers.Select(c => new
            {
                c.CustomerId,
                CustomerFullNameWithEmail = $"{c.CustomerFirstName} {c.CustomerLastName} ({c.CustomerEmail})"
            }).ToList();
            var helpdesks = _context.HelpDesks.Select(h => new
            {
                h.HelpDeskId,
                HelpDeskFullNameWithEmail = $"{h.HelpDeskFirstName} {h.HelpDeskLastName} ({h.HelpDeskEmail})"
            }).ToList();
            var softwareproducts = _context.SoftwareProducts.Select(s => new
            {
                s.SoftwareProductId,
                SoftwareProductIdWithName = $"{s.ProductName} (Product Id: {s.SoftwareProductId})"
            }).ToList();

            ViewBag.Developers = new SelectList(developers, "DeveloperId", "DeveloperFullNameWithEmail");
            ViewBag.Customers = new SelectList(customers, "CustomerId", "CustomerFullNameWithEmail");
            ViewBag.HelpDesks = new SelectList(helpdesks, "HelpDeskId", "HelpDeskFullNameWithEmail");
            ViewBag.SoftwareProducts = new SelectList(softwareproducts, "SoftwareProductId", "SoftwareProductIdWithName");
        }
    }
}
