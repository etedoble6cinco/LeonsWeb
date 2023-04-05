using AutoMapper;
using LeonsWeb.Models;
using LeonsWeb.Models.ViewModel;

namespace LeonsWeb
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<PromoViewModel, Promo>();
            CreateMap<QuoteViewModel, Quote>();
            CreateMap<ServiceViewModel, Service>();
            CreateMap<Service,ServiceViewModel>();
            CreateMap<Promo,PromoViewModel>();
            CreateMap<Quote,QuoteViewModel>();

        
        }
        
    }
}
