using System.ComponentModel.DataAnnotations.Schema;

namespace SSVH_Consultation_Poratl.Models
{
    [Table("transport")]
    public class Transport
    {
        public int Id { get; set; }
        public string? AreaName { get; set; }
        public decimal Amount { get; set; }
    }
}
