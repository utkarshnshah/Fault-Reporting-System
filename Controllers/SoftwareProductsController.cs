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
    public class SoftwareProductsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public SoftwareProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.SoftwareProducts != null ? 
                          View(await _context.SoftwareProducts.ToListAsync()) :
                          Problem("Software Product 'ApplicationDbContext.SoftwareProducts'  not found.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SoftwareProducts == null)
            {
                return NotFound();
            }

            var softwareProduct = await _context.SoftwareProducts
                .FirstOrDefaultAsync(m => m.SoftwareProductId == id);
            if (softwareProduct == null)
            {
                return NotFound();
            }

            return View(softwareProduct);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoftwareProductId,ProductName,ProductDescription")] SoftwareProduct softwareProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(softwareProduct);
                await _context.SaveChangesAsync();
                TempData["SoftwareProductModificationMessage"] = "A new Software Product has been added!";
                return RedirectToAction(nameof(Index));
            }
            return View(softwareProduct);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SoftwareProducts == null)
            {
                return NotFound();
            }

            var softwareProduct = await _context.SoftwareProducts.FindAsync(id);
            if (softwareProduct == null)
            {
                return NotFound();
            }
            return View(softwareProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoftwareProductId,ProductName,ProductDescription")] SoftwareProduct softwareProduct)
        {
            if (id != softwareProduct.SoftwareProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(softwareProduct);
                    await _context.SaveChangesAsync();
                    TempData["SoftwareProductModificationMessage"] = "Software Product has been updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoftwareProductExists(softwareProduct.SoftwareProductId))
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
            return View(softwareProduct);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SoftwareProducts == null)
            {
                return NotFound();
            }

            var softwareProduct = await _context.SoftwareProducts
                .FirstOrDefaultAsync(m => m.SoftwareProductId == id);
            if (softwareProduct == null)
            {
                return NotFound();
            }

            return View(softwareProduct);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SoftwareProducts == null)
            {
                return Problem("Software Product 'ApplicationDbContext.SoftwareProducts' not found.");
            }
            var softwareProduct = await _context.SoftwareProducts.FindAsync(id);
            if (softwareProduct != null)
            {
                _context.SoftwareProducts.Remove(softwareProduct);
                TempData["SoftwareProductModificationMessage"] = "Selected Software Product has been deleted!";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoftwareProductExists(int id)
        {
          return (_context.SoftwareProducts?.Any(e => e.SoftwareProductId == id)).GetValueOrDefault();
        }
    }
}
