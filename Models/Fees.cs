using System.ComponentModel.DataAnnotations.Schema;

namespace SSVH_Consultation_Poratl.Models
{
    [Table("fees")]
    public class Fees
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public int ConsultationId { get; set; }
    }
}
