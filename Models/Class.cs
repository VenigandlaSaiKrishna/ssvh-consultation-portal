using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSVH_Consultation_Poratl.Models
{
    [Table("class")]
    public class Class
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? AcadamicYear { get; set; }
        public decimal AcadamicFees { get; set; }
        public decimal BooksAmount { get; set; }
    }
}
