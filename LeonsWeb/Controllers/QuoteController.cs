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
using LeonsWeb.Services;
using LeonsWeb.Models.QuoteViewModel;

namespace LeonsWeb.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class QuoteController : Controller
    {
        private readonly IQuoteService _quoteservice;
        private readonly IServiceService _serviceservice;
        private readonly IPromoService _promoservice;
        public QuoteController( IQuoteService quoteService , IServiceService serviceService
        , IPromoService promoService)
        {
            _quoteservice = quoteService;
            _serviceservice = serviceService;
            _promoservice = promoService;
        }

        // GET: Quote
        public async Task<IActionResult> Index()
        {
            List<QuoteViewModel> quotes = new List<QuoteViewModel>();
            quotes = await _quoteservice.GetAllQuotes();
            return View(quotes);
        }

        // GET: Quote/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if((await _quoteservice.GetQuote(id)) == null){
                return NotFound();
            }
            return View((await _quoteservice.GetQuote(id)));
        }

        // GET: Quote/Create
        public IActionResult Create()
        {
           ViewData =["PromoId"] = _promoservice.GetSelectList(0);
           ViewData =["ServiceId"] = _serviceservice.GetSelectList(0);
            return View();
        }

        // POST: Quote/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameQuote,AmoutToDiscountQuote,AmountTaxQuote,FinalPrice,IsTaked,PercentToDiscount,ServiceTypeToShow,Comment,ServiceId,PromoId")] QuoteViewModel quoteViewModel)
        {
            if (ModelState.IsValid)
            {
                if( await _quoteservice.CrateQuote(quoteViewModel)){
                    return RedirectToAction(nameof(Index));
                }
                return Problem("Not Created");
            }
          return View(quoteViewModel);
        }

        // GET: Quote/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
         
            var quote = await _quoteservice.GetQuote(id);
            if (quote == null)
            {
                return NotFound();
            }
            ViewData["PromoId"] = 
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", quote.ServiceId);
            return View(quote);
        }

        // POST: Quote/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameQuote,AmoutToDiscountQuote,AmountTaxQuote,FinalPrice,IsTaked,PercentToDiscount,ServiceTypeToShow,Comment,ServiceId,PromoId")] Quote quote)
        {
            if (id != quote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteExists(quote.Id))
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
            ViewData["PromoId"] = new SelectList(_context.Promos, "Id", "Id", quote.PromoId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", quote.ServiceId);
            return View(quote);
        }

        // GET: Quote/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Queues == null)
            {
                return NotFound();
            }

            var quote = await _context.Queues
                .Include(q => q.Promo)
                .Include(q => q.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        // POST: Quote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Queues == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Queues'  is null.");
            }
            var quote = await _context.Queues.FindAsync(id);
            if (quote != null)
            {
                _context.Queues.Remove(quote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteExists(int id)
        {
          return (_context.Queues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
