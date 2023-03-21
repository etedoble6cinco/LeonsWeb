using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeonsWeb.Models.ViewModel
{
    public class ServiceViewModel
    {
        public int Id { get; set; } 
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }
        [Display(Name = "Description")]
        public string ServiceDescription { get; set; }
        [Display(Name = "Price to show")]
        public decimal ServicePrice { get; set; }   
    }
}
