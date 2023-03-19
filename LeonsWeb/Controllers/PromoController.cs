using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeonsWeb.Data;
using LeonsWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace LeonsWeb.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PromoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PromoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Promo
        public async Task<IActionResult> Index()
        {
            
              return _context.Promos != null ? 
                          View(await _context.Promos.ToListAsync()) :
                          Problem("Entity set 'LeonsWebContext.Promo'  is null.");
        }

        // GET: Promo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Promos == null)
            {
                return NotFound();
            }

            var promo = await _context.Promos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promo == null)
            {
                return NotFound();
            }

            return View(promo);
        }

        // GET: Promo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ServiceTypeToShow,StartQuotes,EndQuotes,EndPromoDate,StartPromoDate,NumberOfQuotesTaked,NumberOfQuotesNotTaked,NumberOfQuote")] Promo promo)
        {
            Quote quote = new Quote();
            if(promo.NumberOfQuote > 1)
            {

            }
            if (ModelState.IsValid)
            {
                _context.Add(promo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promo);
        }

        // GET: Promo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Promos == null)
            {
                return NotFound();
            }

            var promo = await _context.Promos.FindAsync(id);
            if (promo == null)
            {
                return NotFound();
            }
            return View(promo);
        }

        // POST: Promo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ServiceTypeToShow,StartQuotes,EndQuotes,EndPromoDate,StartPromoDate,NumberOfQuotesTaked,NumberOfQuotesNotTaked,NumberOfQuote")] Promo promo)
        {
            if (id != promo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromoExists(promo.Id))
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
            return View(promo);
        }

        // GET: Promo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Promos == null)
            {
                return NotFound();
            }

            var promo = await _context.Promos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promo == null)
            {
                return NotFound();
            }

            return View(promo);
        }

        // POST: Promo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Promos == null)
            {
                return Problem("Entity set 'LeonsWebContext.Promo'  is null.");
            }
            var promo = await _context.Promos.FindAsync(id);
            if (promo != null)
            {
                _context.Promos.Remove(promo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromoExists(int id)
        {
          return (_context.Promos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
