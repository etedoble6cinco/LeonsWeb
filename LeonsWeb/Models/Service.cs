using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeonsWeb.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public decimal ServicePrice { get; set; }
      
    }
}
