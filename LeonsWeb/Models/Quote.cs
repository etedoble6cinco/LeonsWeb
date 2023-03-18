using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeonsWeb.Models
{
    public class Quote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NameQuote { get; set; }
        public decimal AmoutToDiscountQuote { get; set; }
        public decimal AmountTaxQuote { get; set; }
        public decimal FinalPrice { get; set; }
        public bool IsTaked { get; set; }
        public decimal PercentToDiscount { get; set; }
        public string ServiceTypeToShow { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Service")] 
        public int ServiceId { get; set; }
        public Service Service { get; set; }   
        [ForeignKey("Promo")]
        public int PromoId { get; set; }    
        public Promo Promo { get; set; }    

        
         
         

        


        
    }
}
