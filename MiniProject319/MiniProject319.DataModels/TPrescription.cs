using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("t_prescription")]
    public partial class TPrescription
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("appointment_id")]
        public long? AppointmentId { get; set; }
        [Column("medical_item_id")]
        public long? MedicalItemId { get; set; }
        [Column("dosage", TypeName = "text")]
        public string? Dosage { get; set; }
        [Column("directions", TypeName = "text")]
        public string? Directions { get; set; }
        [Column("time")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Time { get; set; }
        [Column("notes", TypeName = "text")]
        public string? Notes { get; set; }
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
