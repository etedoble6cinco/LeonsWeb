using LeonsWeb.Data;
using LeonsWeb.Models;
using LeonsWeb.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LeonsWeb.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;

        public ServiceService(ApplicationDbContext context)
        { 
          _context = context;   
        } 
        public async Task<bool> CrateService(ServiceViewModel serviceViewModel)
        {
            try
            {
                Service service = new Service();
                service.Id = serviceViewModel.Id;
                service.ServiceName = serviceViewModel.ServiceName;
                service.ServicePrice = serviceViewModel.ServicePrice;
                service.ServiceDescription = serviceViewModel.ServiceDescription;
                _context.Add(service);
 
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());   
                throw;
             }  

            
        }

        public async Task<bool> DeleteService(int? id)
        {
            try
            {

                var service = await _context.Services.FindAsync(id);
                _context.Remove(service);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
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
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public async Task<List<ServiceViewModel>> GetAllServices()
        {
           try {

                List<ServiceViewModel> serviceViewModels = new List<ServiceViewModel>();
                var services = (from _service in
                                    _context.Services
                                join _quotes in _context.Queues on _service.Id equals _quotes.ServiceId
                                select new ServiceViewModel
                                {
                                    Id = _service.Id,
                                    ServiceDescription = _service.ServiceDescription,
                                    ServiceName = _service.ServiceName,
                                    ServicePrice = _service.ServicePrice
                                }).OrderBy(x => x.Id);

                return await services.ToListAsync();
            } catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                throw;
            }
            

        }

        public async Task<ServiceViewModel> GetService(int? id)
        {
            try
            {
                  if(await _context.Services.FirstOrDefaultAsync(x => x.Id == id) is not null)
                {
                    var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);

                    ServiceViewModel serviceViewModel = new ServiceViewModel();
                    serviceViewModel.Id = service.Id;
                    serviceViewModel.ServicePrice = service.ServicePrice;
                    serviceViewModel.ServiceDescription = service.ServiceDescription;
                    serviceViewModel.ServiceName = service.ServiceName;
                    return serviceViewModel;

                }
                else
                {
                    return new ServiceViewModel();
                }            
              
                
            }catch (Exception e)
            {
                throw;
            }
        }


    }
}
