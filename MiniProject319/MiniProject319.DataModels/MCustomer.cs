using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("m_customer")]
    public partial class MCustomer
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("biodata_id")]
        public long? BiodataId { get; set; }
        [Column("dob", TypeName = "date")]
        public DateTime? Dob { get; set; }
        [Column("gender")]
        [StringLength(1)]
        [Unicode(false)]
        public string? Gender { get; set; }
        [Column("blood_group_id")]
        public long? BloodGroupId { get; set; }
        [Column("rhesus_type")]
        [StringLength(5)]
        [Unicode(false)]
        public string? RhesusType { get; set; }
        [Column("height", TypeName = "decimal(18, 0)")]
        public decimal? Height { get; set; }
        [Column("weight", TypeName = "decimal(18, 0)")]
        public decimal? Weight { get; set; }
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
