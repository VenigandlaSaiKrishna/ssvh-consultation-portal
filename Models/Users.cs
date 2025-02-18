using System.ComponentModel.DataAnnotations.Schema;

namespace SSVH_Consultation_Poratl.Models
{
    [Table("users")]
    public class Users
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool Status { get; set; }
    }
}
