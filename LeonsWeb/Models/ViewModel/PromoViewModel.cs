using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeonsWeb.Models.ViewModel
{
    public class PromoViewModel
    {
        public int Id { get; set; }
        [Display(Name="Promotion campaing name")]
        public string Name { get; set; }
        [Display(Name= "Description of the campaing promotion")]
        public string Description { get; set; }
        [Display (Name="Description of the service type to show in promotion")]
        public string ServiceTypeToShow { get; set; }
        [Display (Name ="Date range of Quotes")]
        public string DateRangeOfQuotes { get; set; }
        [Display(Name ="Start Date to show Quotes")]
        public DateTime StartQuotes { get; set; }
        [Display(Name = "End Date to show Quotes")]
        public DateTime EndQuotes { get; set; }
        [Display(Name ="Start Promotion campaing Date")]
        public DateTime EndPromoDate { get; set; }
        [Display(Name=" End Promotion campaing Date")]
        public DateTime StartPromoDate { get; set; }
        [Display(Name="Number of Quotes actually taked")]
        public int NumberOfQuotesTaked { get; set; }
        [Display(Name="Number of Quotes actually not Taked")]
        public int NumberOfQuotesNotTaked { get; set; }
        [Display(Name="Number of Quote inside of the promotion campaing")]
        public int NumberOfQuote { get; set; }
    }
}
