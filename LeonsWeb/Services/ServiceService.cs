using AutoMapper;
using LeonsWeb.Data;
using LeonsWeb.Models;
using LeonsWeb.Models.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LeonsWeb.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ServiceService(ApplicationDbContext context ,IMapper mapper)
        { 
          _context = context;   
          _mapper = mapper;
        } 
        public async Task<bool> CreateService(ServiceViewModel serviceViewModel)
        {
            try
            {
            
                var service = _mapper.Map<ServiceViewModel, Service>(serviceViewModel);
                _context.Add(service);
 
                await _context.SaveChangesAsync();
                return true;
            } catch (SqlException ex) {
                Console.WriteLine(ex.ToString());   
                return false;
             }  

            
        }

        public async Task<bool> DeleteService(int? id)
        {
            try
            {    
                if(id is not null && ServiceExists(id)){}
                var service = await _context.Services.FindAsync(id);
                _context.Remove(service);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }


        public async Task<bool> EditService(ServiceViewModel serviceViewModel)
        {
            try
            {
                var service = await _context.Services.FindAsync(serviceViewModel.Id);
                service.ServiceName = serviceViewModel.ServiceName;
                service.ServiceDescription= serviceViewModel.ServiceDescription;
                service.ServicePrice = serviceViewModel.ServicePrice;
                
                _context.Update(service);
                await _context.SaveChangesAsync();
                return true;
            }catch (DbUpdateConcurrencyException ex)
               {
                   Console.WriteLine(ex.Message.ToString());
                   return false;
               }
        }

        public async Task<List<ServiceViewModel>> GetAllServices()
        {
           try {

                 var services =  await _context.Services.ToListAsync();
                 var servicesVM = _mapper.Map<List<Service>, List<ServiceViewModel>>(services);

                 return servicesVM;
               
            } catch (Exception e)
            {

                List<ServiceViewModel> servicesVM = new List<ServiceViewModel>();
                return servicesVM;
            }
            

        }

        public async Task<ServiceViewModel> GetService(int? id)
        {
            try
            {
                if(ServiceExists(id)){
                   var service = await _context.Services.FindAsync(id);   
                   var servicesVM = _mapper.Map<Service,ServiceViewModel>(service);
                   return servicesVM;
                }
                return null;
            }catch (SqlException e)
            {
                Console.WriteLine(e.Message.ToString());
                throw;
            }
        }
         private bool ServiceExists(int? id)
        {
            if(id is not null){
                    return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
            }else{
                return false;
            }
          
        }


    }
}
