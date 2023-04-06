using LeonsWeb.Models;
using LeonsWeb.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeonsWeb.Services
{
    public interface IServiceService
    {
        Task<bool> CreateService(ServiceViewModel serviceViewModel);
        Task<ServiceViewModel> GetService(int? id);
        Task<List<ServiceViewModel>> GetAllServices(); 
        
        Task<bool> EditService(ServiceViewModel serviceViewModel);  
        Task<bool> DeleteService(int? id);   

        List<Service> GetList();
        
    
    }
}
