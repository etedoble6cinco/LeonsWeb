
using Microsoft.AspNetCore.Mvc;
using LeonsWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using LeonsWeb.Services;

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
           ViewData["PromoId"] = _promoservice.GetSelectList(0);
           ViewData["ServiceId"] = _serviceservice.GetSelectList(0);
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
            ViewData["PromoId"] =   _promoservice.GetSelectList(quote.PromoId);
            ViewData["ServiceId"] =  _serviceservice.GetSelectList(quote.ServiceId);
            return View(quote);
        }

        // POST: Quote/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameQuote,AmoutToDiscountQuote,AmountTaxQuote,FinalPrice,IsTaked,PercentToDiscount,ServiceTypeToShow,Comment,ServiceId,PromoId")] QuoteViewModel quoteViewModel)
        {
             if (id != quoteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(await _quoteservice.EditQuote(quoteViewModel))
                return RedirectToAction(nameof(Index));
                else{
                    return NotFound();
                }
            }
        return View(quoteViewModel);
        
        }
         
        
        

        // GET: Quote/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
                if ( await _quoteservice.GetQuote(id) == null)
            {
                return NotFound();
            }

            return View(await _quoteservice.GetQuote(id));
      
        }

        // POST: Quote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             if ((await _quoteservice.DeleteQuote(id)))
            {
                return RedirectToAction(nameof(Index));
                
            }
        
         return Problem("Entity set 'ApplicationDbContext.Services'  is null.");   
       
        }

    }
}
