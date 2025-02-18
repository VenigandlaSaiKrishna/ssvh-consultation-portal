using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSVH_Consultation_Poratl.Models
{
    [Table("classnames")]
    public class ClassNames
    {
        [Key]
        public int Id { get; set; }
        public string? ClassName { get; set; }
    }
}