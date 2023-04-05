using AutoMapper;
using LeonsWeb.Data;
using LeonsWeb.Models;
using LeonsWeb.Models.ViewModel;
using LeonsWeb.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }


        }

        public async Task<bool> DeleteQuote(int? id)
        {
            try
            {
                
                if(id is not null && QuouteExists(id)){
                    var service = await _context.Queues.FindAsync(id);
                    _context.Remove(service);
                    await _context.SaveChangesAsync();
                    return true ;
                }
                
                return false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }


        public async Task<bool> EditQuote(QuoteViewModel quoteViewModel)
        {
            try
            {

                var quote= _mapper.Map<QuoteViewModel,Quote>(quoteViewModel);
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
            QuoteViewModel quoteEmpty = new QuoteViewModel();
            try
            {
               if(QuouteExists(id)){
                    var quote = await _context.Queues.FindAsync(id);
                    var quoteVM = _mapper.Map<Quote,QuoteViewModel>(quote);
                    return quoteVM;
               }
               
               return quoteEmpty;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            return quoteEmpty;
            }

        }

        public async Task<List<QuoteViewModel>> GetAllQuotes()
        {
            try{

                List<Quote> quotes = new List<Quote>();
                quotes = await _context.Queues.ToListAsync();
                var quotesVM = _mapper.Map<List<Quote>, List<QuoteViewModel>>(quotes);
                return quotesVM;

            }catch(SqlException ex) {
                
                Console.WriteLine(ex.Message.ToString());
                List<QuoteViewModel> quotes =new List<QuoteViewModel>();
                return quotes;
            }
      
        }


           private bool QuouteExists(int? id)
        {
            if(id is not null){
                    return (_context.Queues?.Any(e => e.Id == id)).GetValueOrDefault();
            }else{
                return false;
            }
          
        }
        public SelectList GetSelectList(int id ){

            if(id >0){
            SelectList selectList = new SelectList(_context.Queues, "Id", "Name", id);
            return selectList;
            }else{
            SelectList selectList = new SelectList(_context.Queues, "Id", "Name");
            return selectList;       
            }
         
        }
    }

}
