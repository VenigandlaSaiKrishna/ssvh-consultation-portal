using System.ComponentModel.DataAnnotations.Schema;

namespace SSVH_Consultation_Poratl.Models
{
    [Table("roles")]
    public class Roles
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
