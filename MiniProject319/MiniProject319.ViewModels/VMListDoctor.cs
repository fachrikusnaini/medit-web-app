using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMListDoctor
    {
        public long? DoctorId { get; set; }
        public string? NameDoctor { get; set; }
        public string? ImagePath { get; set; }
        public long? SpecializationId { get; set; }
        public string? NameSpecialist { get; set; }
        //public long MedicalFacilityId { get; set; }
        public List<VMRiwayatPraktek>? RiwayatPraktek { get; set; }
        public List<VMTindakanMedis>? ListTindakan { get; set; }

    }
}
