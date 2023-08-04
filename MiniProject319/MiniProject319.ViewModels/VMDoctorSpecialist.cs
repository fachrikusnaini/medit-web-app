﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiniProject319.ViewModels
{
    public class VMDoctorSpecialist
    {
        public long? Id { get; set; }
        public long? DoctorId { get; set; }
        public long? BiodataId { get; set; }
        public string? Str { get; set; }
        public long? SpecializationId { get; set; }
        public string? SpecialistName { get; set; }
        public string? Fullname { get; set; }
        public string? MobilePhone { get; set; }
        public int? CountAppointment { get; set; }
        public int? CountChat { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }
        public VMItemMedical? itemMedical { get; set; }
        public List<VMTindakanMedis>? ListTindakan { get; set; }
        public List<VMRiwayatPraktek>? RiwayatPraktek { get; set; }
        public List<VMJadwalPraktek>? JadwalPraktek { get; set; }
        public List<VMPendidikanDokter>? PendidikanDokter { get; set; }
    }
}