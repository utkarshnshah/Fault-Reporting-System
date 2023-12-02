using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FaultReportingSystem.Data;
using FaultReportingSystem.Models;
using FaultReportingSystem.ViewModels;

namespace FaultReportingSystem
{
    public class ReportsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> Index()
        {
            PopulateDropdowns();
            return _context.Faults != null ?
                        View(await _context.Faults.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Managers'  is null.");
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> AllOpenFaults()
        {
            PopulateDropdowns();
            var OpenFaults = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Where(f => f.Status == "Open").ToListAsync();
            return View(OpenFaults);
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> AllClosedFaults()
        {
            PopulateDropdowns();
            var ClosedFaults = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Where(f => f.Status == "Solved")
                .ToListAsync();

            return View(ClosedFaults);
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> OpenFaultsAssignedToDeveloper(int? SelectedDeveloperId)
        {
            PopulateDropdowns();
            if (SelectedDeveloperId == null)
            {
                PopulateDropdowns();
                return View();
            }
            var openFaultsAssignedToDeveloper = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Where(f => f.DeveloperId == SelectedDeveloperId && f.Status == "Open")
                .ToListAsync();

            var DeveloperName = ((SelectList)ViewBag.Developers)
                .FirstOrDefault(item => item.Value == SelectedDeveloperId.ToString())?
                .Text;

            ViewBag.OpenFaultsAssignedToDeveloperName = DeveloperName;
            return View(openFaultsAssignedToDeveloper);
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> OpenFaultsAssignedToHelpDesk(int? SelectedHelpDeskStaffId)
        {
            PopulateDropdowns();
            if (SelectedHelpDeskStaffId == null)
            {
                PopulateDropdowns();
                return View();
            }
            var openFaultsAssignedToHelpDesk = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Where(f => f.HelpDeskId == SelectedHelpDeskStaffId && f.Status == "Open")
                .ToListAsync();

            var HelpDeskStaffName = ((SelectList)ViewBag.HelpDesks)
                .FirstOrDefault(item => item.Value == SelectedHelpDeskStaffId.ToString())?
                .Text;

            ViewBag.OpenFaultsAssignedToHelpDeskStaffName = HelpDeskStaffName;
            return View(openFaultsAssignedToHelpDesk);
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> ClosedFaultsAssignedToDeveloper(int? SelectedDeveloperId)
        {
            PopulateDropdowns();
            if (SelectedDeveloperId == null)
            {
                PopulateDropdowns();
                return View();
            }
            var closedFaultsAssignedToDeveloper = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Where(f => f.DeveloperId == SelectedDeveloperId && f.Status == "Solved")
                .ToListAsync();

            var DeveloperName = ((SelectList)ViewBag.Developers)
                .FirstOrDefault(item => item.Value == SelectedDeveloperId.ToString())?
                .Text;

            ViewBag.ClosedFaultsAssignedToDeveloperName = DeveloperName;
            return View(closedFaultsAssignedToDeveloper);
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> ClosedFaultsAssignedToHelpDesk(int? SelectedHelpDeskStaffId)
        {
            PopulateDropdowns();
            if (SelectedHelpDeskStaffId == null)
            {
                PopulateDropdowns();
                return View();
            }
            var closedFaultsAssignedToHelpDesk = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Where(f => f.HelpDeskId == SelectedHelpDeskStaffId && f.Status == "Solved")
                .ToListAsync();

            var HelpDeskStaffName = ((SelectList)ViewBag.HelpDesks)
                .FirstOrDefault(item => item.Value == SelectedHelpDeskStaffId.ToString())?
                .Text;

            ViewBag.ClosedFaultsAssignedToHelpDeskStaffName = HelpDeskStaffName;
            return View(closedFaultsAssignedToHelpDesk);
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> AllFaultsReportedForAProduct(int? SelectedSoftwareProductId)
        {
            PopulateDropdowns();
            if (SelectedSoftwareProductId == null)
            {
                PopulateDropdowns();
                return View();
            }
            var allFaultsReportedForAProduct = await _context.Faults
                .Include(f => f.Customer)
                .Include(f => f.Developer)
                .Include(f => f.HelpDesk)
                .Include(f => f.SoftwareProduct)
                .Where(f => f.SoftwareProductId == SelectedSoftwareProductId)
                .ToListAsync();

            var SoftwareProductName = ((SelectList)ViewBag.SoftwareProducts)
                .FirstOrDefault(item => item.Value == SelectedSoftwareProductId.ToString())?
                .Text;

            ViewBag.AllFaultsReportedForAProductName = SoftwareProductName;
            return View(allFaultsReportedForAProduct);
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public async Task<IActionResult> DevelopersAssignedToManagers()
        {
            var managers = await _context.Managers.ToListAsync();
            var viewModel = new List<ManagerDeveloperFaultViewModel>();

            foreach (var manager in managers)
            {
                var developerTasks = _context.Developers
                    .Where(d => d.ManagerId == manager.ManagerId)
                    .Select(dev => new DeveloperFaultViewModel
                    {
                        DeveloperId = dev.DeveloperId,
                        DeveloperNameWithEmail = $"{dev.DeveloperFirstName} {dev.DeveloperLastName} ({dev.DeveloperEmail})",
                        OpenFaultCount = _context.Faults.Count(f => f.DeveloperId == dev.DeveloperId && f.Status == "Open")
                    })
                    .ToList();

                var managerViewModel = new ManagerDeveloperFaultViewModel
                {
                    ManagerId = manager.ManagerId,
                    ManagerNameWithEmail = $"{manager.ManagerFirstName} {manager.ManagerLastName} ({manager.ManagerEmail})",
                    Developers = developerTasks
                };

                viewModel.Add(managerViewModel);
            }
            return View(viewModel);
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
        public IActionResult Profile()
        {
            if (IsUserLoggedIn())
            {
                SetViewDataFromSession();
                return View();
            }
            else
            {
                ClearUserSession();
                return RedirectToAction("Login", "Home");
            }
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