using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("t_doctor_office_schedule")]
    public partial class TDoctorOfficeSchedule
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("doctor_id")]
        public long? DoctorId { get; set; }
        [Column("medical_facility_schedule_id")]
        public long? MedicalFacilityScheduleId { get; set; }
        [Column("slot")]
        public int? Slot { get; set; }
        [Column("created_by")]
        public long CreatedBy { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("modified_by")]
        public long? ModifiedBy { get; set; }
        [Column("modified_on", TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("deleted_by")]
        public long? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_delete")]
        public bool IsDelete { get; set; }
    }
}
