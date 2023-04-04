using AutoMapper;
using LeonsWeb.Data;
using LeonsWeb.Models;
using LeonsWeb.Models.QuoteViewModel;
using LeonsWeb.Models.ViewModel;
using LeonsWeb.Services;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LeonsWeb.Quotes
{
    public class QuoteService : IQuoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public QuoteService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<bool> CrateQuote(QuoteViewModel quoteViewModel)
        {
            try
            {
                var quote = _mapper.Map<QuoteViewModel , Quote>(quoteViewModel);
                
               
                _context.Add(quote);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }


        }

        public async Task<bool> DeleteQuote(int? id)
        {
            try
            {
                
                var Quote = await _context.Queues.FindAsync(id);
                _context.Remove(Quote);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

        }


        public async Task<bool> EditQuote(QuoteViewModel quoteViewModel)
        {
            try
            {
                var quote = await _context.Queues.FindAsync(quoteViewModel.Id);
             
                _context.Update(quote);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

   

        public async Task<QuoteViewModel> GetQuote(int? id)
        {
            try
            {
                if (await _context.Queues.FirstOrDefaultAsync(x => x.Id == id) is not null)
                {
                    var quote = await _context.Queues.FirstOrDefaultAsync(x => x.Id == id);

                    QuoteViewModel quoteViewModel = new QuoteViewModel();
               
                    return quoteViewModel;

                }
                else
                {
                    return new QuoteViewModel();
                }


            }
            catch (Exception e)
            {
                throw;
            }
        }

        Task<List<ServiceViewModel>> IQuoteService.GetAllQuotes()
        {
            throw new NotImplementedException();
        }

        Task<ServiceViewModel> IQuoteService.GetQuote(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
