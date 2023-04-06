using LeonsWeb.Models;
using LeonsWeb.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeonsWeb.Services
{
    public interface IPromoService
    {
        Task<bool> CratePromo(PromoViewModel PromoViewModel);
        Task<PromoViewModel> GetPromo(int? id);
        Task<List<PromoViewModel>> GetAllPromos();

        Task<bool> EditPromo(PromoViewModel PromoViewModel);
        Task<bool> DeletePromo(int? id);
        List<Promo> GetList();
    }
}
