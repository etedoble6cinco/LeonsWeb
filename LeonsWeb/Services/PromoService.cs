using AutoMapper;
using LeonsWeb.Data;
using LeonsWeb.Models;
using LeonsWeb.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LeonsWeb.Services
{
     public class PromoService : IPromoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PromoService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<bool> CratePromo(PromoViewModel PromoViewModel)
        {
            try
            {
                var Promo = _mapper.Map<PromoViewModel , Promo>(PromoViewModel);
                
               
                _context.Add(Promo);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }


        }

        public async Task<bool> DeletePromo(int? id)
        {
            try
            {
                
                if(id is not null && QuouteExists(id)){
                    var service = await _context.Promos.FindAsync(id);
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


        public async Task<bool> EditPromo(PromoViewModel PromoViewModel)
        {
            try
            {

                var Promo= _mapper.Map<PromoViewModel,Promo>(PromoViewModel);
                _context.Update(Promo);
                await _context.SaveChangesAsync();
                return true;
            }
              
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

   

        public async Task<PromoViewModel> GetPromo(int? id)
        {
            PromoViewModel PromoEmpty = new PromoViewModel();
            try
            {
               if(QuouteExists(id)){
                    var Promo = await _context.Promos.FindAsync(id);
                    var PromoVM = _mapper.Map<Promo,PromoViewModel>(Promo);
                    return PromoVM;
               }
               
               return PromoEmpty;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            return PromoEmpty;
            }

        }

        public async Task<List<PromoViewModel>> GetAllPromos()
        {
            try{

                List<Promo> Promos = new List<Promo>();
                Promos = await _context.Promos.ToListAsync();
                var PromosVM = _mapper.Map<List<Promo>, List<PromoViewModel>>(Promos);
                return PromosVM;

            }catch(SqlException ex) {
                
                Console.WriteLine(ex.Message.ToString());
                List<PromoViewModel> Promos =new List<PromoViewModel>();
                return Promos;
            }
      
        }


           private bool QuouteExists(int? id)
        {
            if(id is not null){
                    return (_context.Promos?.Any(e => e.Id == id)).GetValueOrDefault();
            }else{
                return false;
            }
          
        }

        public SelectList GetSelectList(int id){


            SelectList selectList = new SelectList(_context.Promos, "Id", "Name", id);
            return selectList;
        }

     
    }
}
