using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace SSVH_Consultation_Poratl.Models
{
    public class CreateConsultationViewModel
    {
        public int Id { get; set; }
        public string? StudentName { get; set; }
        public string? ParentName { get; set; }
        public string? Mobile { get; set; }
        public string? PreviousSchoolName { get; set; }
        public decimal? FullAmount { get; set; }
        public string? AcadamicYear { get; set; }
        public string? ClassName { get; set; }
        public int? ClassId { get; set; }
        public int? StationaryId { get; set; }
        public string? TransportId { get; set; }
        public decimal? TransportAmount { get; set; }
        public decimal? AcadamicFees { get; set; }
        public bool IsAcadamicFeesDiscount { get; set; }
        public decimal? AcadamicFeesDiscount { get; set; }
        public decimal? UniformAmount { get; set; }
        public string? UniformData { get; set; }
        public decimal? BooksAmount { get; set; }
        public decimal? AdmissionFees { get; set; }
        public string? Status { get; set; }
        public decimal? BeltTieSocksAmount { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
