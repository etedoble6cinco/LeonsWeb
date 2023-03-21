using LeonsWeb.Models.QuoteViewModel;
using LeonsWeb.Models.ViewModel;

namespace LeonsWeb.Services
{
    public interface IQuoteService
    {
        Task<bool> CrateQuote(QuoteViewModel quoteViewModel);
        Task<ServiceViewModel> GetQuote(int? id);
        Task<List<ServiceViewModel>> GetAllQuotes();

        Task<bool> EditQuote(QuoteViewModel quoteViewModel);
        Task<bool> DeleteQuote(int? id);

    }
}
