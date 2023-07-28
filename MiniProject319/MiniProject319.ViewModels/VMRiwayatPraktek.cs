using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMRiwayatPraktek
    {
        public long? DoctorId { get; set; }
        public long? MedicalFacilityId { get; set; }
        public string? MedicalFacilityName { get; set; }
        public string Specialization { get; set; } = null!;
        public string? SpecialistName { get; set; }
        public string? Location { get; set; }
        public string? FullAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LamaBekerja { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceStartFrom { get; set; }
        public decimal? PriceUntilFrom { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}
