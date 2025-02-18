using System.ComponentModel.DataAnnotations.Schema;

namespace SSVH_Consultation_Poratl.Models
{
    [Table("stationary")]
    public class Stationary
    {
        public int Id { get; set; }
        public string? Gender { get; set; }
        public string? ClassName { get; set; }
        public string? ShirtSize { get; set; }
        public string? PantSize { get; set; }
        public string? SkirtSize { get; set; }
        public decimal? Amount { get; set; }
        public decimal? BeltTieSocksAmount { get; set; }
    }
}
