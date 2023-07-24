using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("m_doctor_education")]
    public partial class MDoctorEducation
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("doctor_id")]
        public long? DoctorId { get; set; }
        [Column("education_level_id")]
        public long? EducationLevelId { get; set; }
        [Column("institution_name")]
        [StringLength(100)]
        [Unicode(false)]
        public string? InstitutionName { get; set; }
        [Column("major")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Major { get; set; }
        [Column("start_year")]
        [StringLength(4)]
        [Unicode(false)]
        public string? StartYear { get; set; }
        [Column("end_year")]
        [StringLength(4)]
        [Unicode(false)]
        public string? EndYear { get; set; }
        [Column("is_last_education")]
        public bool IsLastEducation { get; set; }
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
