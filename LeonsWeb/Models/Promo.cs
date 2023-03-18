using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeonsWeb.Models
{
    public class Promo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string ServiceTypeToShow { get; set; }
        public DateTime StartQuotes { get; set; }
        public DateTime EndQuotes { get; set; }
        public DateTime EndPromoDate { get; set; }
        public DateTime StartPromoDate { get; set; }
        public int NumberOfQuotesTaked { get; set; }
        public int NumberOfQuotesNotTaked { get; set; }
        public int NumberOfQuote { get; set; }
        public virtual ICollection<Quote> Quotes { get; }    
    }
}
