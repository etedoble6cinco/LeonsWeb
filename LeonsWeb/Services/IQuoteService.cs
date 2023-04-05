
using LeonsWeb.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeonsWeb.Services
{
    public interface IQuoteService
    {
        Task<bool> CrateQuote(QuoteViewModel quoteViewModel);
        Task<QuoteViewModel> GetQuote(int? id);
        Task<List<QuoteViewModel>> GetAllQuotes();

        Task<bool> EditQuote(QuoteViewModel quoteViewModel);
        Task<bool> DeleteQuote(int? id);

        SelectList GetSelectList(int id);

    }
}
