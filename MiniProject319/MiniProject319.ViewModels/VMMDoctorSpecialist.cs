using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMMDoctorSpecialist
    {
        public long? DoctorId { get; set; }
        public long? BiodataId { get; set; }
        public string? Str { get; set; }
        public long? SpecializationId { get; set; }
        public string? SpecialistName { get; set; }
        public string? Fullname { get; set; }
        public string? MobilePhone { get; set; }
        public int? CountAppointment { get; set; }
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        public List<VMMTindakanMedis>? ListTindakan { get; set; }
        public List<VMMRiwayatPraktek>? RiwayatPraktek { get; set; }
        public List<VMMPendidikanDoctor>? PendidikanDokter { get; set; }
    }
}
