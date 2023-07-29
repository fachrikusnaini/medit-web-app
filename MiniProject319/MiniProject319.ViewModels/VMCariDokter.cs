using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMCariDokter
    {
        //public long? DoctorId { get; set; }
        //public string? DoctorName { get; set; }
        public List<VMGetSpecialist>? GetSpecialists { get; set; }
        public List<VMGetLocation>? GetLocations { get; set; }
        public List<VMGetTreatment>? GetTreatments { get; set; }
    }
}
