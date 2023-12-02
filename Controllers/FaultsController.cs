using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FaultReportingSystem.Data;
using FaultReportingSystem.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FaultReportingSystem.Controllers
{
    public class FaultsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public FaultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            PopulateDropdowns();
            var applicationDbContext = _context.Faults.Include(f => f.Customer).Include(f => f.Developer).Include(f => f.HelpDesk).Include(f => f.SoftwareProduct);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            PopulateDropdowns();
            if (id == null || _context.Faults == null)
            {
                return NotFound();
            }

            var fault = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        public IActionResult Create()
        {
            PopulateDropdowns();

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId");
            ViewData["HelpDeskId"] = new SelectList(_context.HelpDesks, "HelpDeskId", "HelpDeskId");
            ViewData["SoftwareProductId"] = new SelectList(_context.SoftwareProducts, "SoftwareProductId", "SoftwareProductId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaultId,DateReported,Priority,Problem,Description,Status,ClosedOn,FaultType,ReportMethod,CustomerId,DeveloperId,HelpDeskId,SoftwareProductId")] Fault fault)
        {
            PopulateDropdowns();

            if (ModelState.IsValid)
            {
                _context.Add(fault);
                await _context.SaveChangesAsync();
                TempData["FaultModificationMessage"] = "Fault has been reported successfully!";

                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", fault.CustomerId);
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId", fault.DeveloperId);
            ViewData["HelpDeskId"] = new SelectList(_context.HelpDesks, "HelpDeskId", "HelpDeskId", fault.HelpDeskId);
            ViewData["SoftwareProductId"] = new SelectList(_context.SoftwareProducts, "SoftwareProductId", "SoftwareProductId", fault.SoftwareProductId);
            return View(fault);
        }

        public IActionResult CreateFault()
        {
            PopulateDropdowns();
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId");
            ViewData["HelpDeskId"] = new SelectList(_context.HelpDesks, "HelpDeskId", "HelpDeskId");
            ViewData["SoftwareProductId"] = new SelectList(_context.SoftwareProducts, "SoftwareProductId", "SoftwareProductId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFaultByCustomer([Bind("FaultId,DateReported,Priority,Problem,Description,Status,ClosedOn,FaultType,ReportMethod,CustomerId,DeveloperId,HelpDeskId,SoftwareProductId")] Fault fault)
        {
            PopulateDropdowns();
            fault.DateReported = DateTime.Now;
            fault.HelpDeskId = null;
            fault.DeveloperId = null;
            fault.ClosedOn = null;
            fault.CustomerId = ViewData["LoggedUserId"] as int?;
            if (ModelState.IsValid)
            {
                _context.Add(fault);
                await _context.SaveChangesAsync();
                TempData["FaultModificationMessage"] = "Thanks for reporting the Fault. We will get back to you shortly.";

                return RedirectToAction("MyFaults");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", fault.CustomerId);
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId", fault.DeveloperId);
            ViewData["HelpDeskId"] = new SelectList(_context.HelpDesks, "HelpDeskId", "HelpDeskId", fault.HelpDeskId);
            ViewData["SoftwareProductId"] = new SelectList(_context.SoftwareProducts, "SoftwareProductId", "SoftwareProductId", fault.SoftwareProductId);
            return View("CreateFault", fault);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            PopulateDropdowns();

            if (id == null || _context.Faults == null)
            {
                return NotFound();
            }

            var fault = await _context.Faults.FindAsync(id);
            if (fault == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", fault.CustomerId);
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId", fault.DeveloperId);
            ViewData["HelpDeskId"] = new SelectList(_context.HelpDesks, "HelpDeskId", "HelpDeskId", fault.HelpDeskId);
            ViewData["SoftwareProductId"] = new SelectList(_context.SoftwareProducts, "SoftwareProductId", "SoftwareProductId", fault.SoftwareProductId);
            return View(fault);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaultId,DateReported,Priority,Problem,Description,Status,ClosedOn,FaultType,ReportMethod,CustomerId,DeveloperId,HelpDeskId,SoftwareProductId")] Fault fault)
        {
            PopulateDropdowns();

            if (id != fault.FaultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fault);
                    await _context.SaveChangesAsync();
                    TempData["FaultModificationMessage"] = "Fault details has been updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultExists(fault.FaultId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                string? ActiveUserType = ViewData["LoggedUserType"] as string;

                if (ActiveUserType == "Developer")
                {
                    return RedirectToAction("DevelopersFaults");
                }
                if (ActiveUserType == "Manager")
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", fault.CustomerId);
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId", fault.DeveloperId);
            ViewData["HelpDeskId"] = new SelectList(_context.HelpDesks, "HelpDeskId", "HelpDeskId", fault.HelpDeskId);
            ViewData["SoftwareProductId"] = new SelectList(_context.SoftwareProducts, "SoftwareProductId", "SoftwareProductId", fault.SoftwareProductId);
            return View(fault);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Faults == null)
            {
                return NotFound();
            }

            var fault = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Faults == null)
            {
                return Problem("Fault 'ApplicationDbContext.Faults' not found.");
            }
            var fault = await _context.Faults.FindAsync(id);
            if (fault != null)
            {
                _context.Faults.Remove(fault);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> MyFaults()
        {
            int? CurrentCustomerId = ViewData["LoggedUserId"] as int?;
            PopulateDropdowns();
            if (CurrentCustomerId == null)
            {
                PopulateDropdowns();
                return View();
            }
            var AllCustomersFaults = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Where(f => f.CustomerId == CurrentCustomerId)
                .ToListAsync();

            return View(AllCustomersFaults);
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> DevelopersFaults()
        {
            int? CurrentDeveloperId = ViewData["LoggedUserId"] as int?;
            PopulateDropdowns();
            if (CurrentDeveloperId == null)
            {
                PopulateDropdowns();
                return View();
            }
            var AllDevelopersFaults = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Where(f => f.DeveloperId == CurrentDeveloperId)
                .ToListAsync();
            return View(AllDevelopersFaults);
        }
        private bool FaultExists(int id)
        {
            return (_context.Faults?.Any(e => e.FaultId == id)).GetValueOrDefault();
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
