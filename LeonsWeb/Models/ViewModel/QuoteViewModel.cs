using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace LeonsWeb.Models.ViewModel
{
    public class QuoteViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Name to idenify the quote (not to show)")]

        public string NameQuote { get; set; }
        [Display(Name ="Amount of discount to show in promotion of the quote")]
        public decimal AmoutToDiscountQuote { get; set; }
        [Display(Name = "Amount of tax to show in promotion of the quote")]
        public decimal AmountTaxQuote { get; set; }
        [Display(Name ="Final amount of price to show in promotion of the quote")]
        public decimal FinalPrice { get; set; }
        [Display(Name ="Is already Taked?")]
        public bool IsTaked { get; set; }
        [Display(Name ="Percent of discount to show in promotion of the quote")]
        public decimal PercentToDiscount { get; set; }
        [Display(Name ="Service type to show in promotion of the quote")]
        public string ServiceTypeToShow { get; set; }
        [Display(Name ="Comment to show in promotion of the quote")]
        public string Comment { get; set; }
       
        public int ServiceId { get; set; }
        [Display(Name ="Service linked to the quote (not to show)")]
        public string ServiceName { get; set; }
        public Service services { get; set; }  
        public int PromoId { get; set; }
        [Display(Name ="Promotion campaing name for the quote")]
        public string PromoName { get; set; }
        public Promo promo { get; set; }    


     
    }
}
