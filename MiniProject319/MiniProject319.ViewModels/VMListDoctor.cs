using System;
using System.Collections.Generic;
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
        public long? SpecializationId { get; set; }
        public string? NameSpecialist { get; set; }

    }
}
