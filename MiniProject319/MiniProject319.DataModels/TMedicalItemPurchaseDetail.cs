using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("t_medical_item_purchase_detail")]
    public partial class TMedicalItemPurchaseDetail
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("medical_item_purchase_id")]
        public long? MedicalItemPurchaseId { get; set; }
        [Column("medical_item_id")]
        public long? MedicalItemId { get; set; }
        [Column("qty")]
        public int? Qty { get; set; }
        [Column("medical_facility_id")]
        public long? MedicalFacilityId { get; set; }
        [Column("courir_id")]
        public long? CourirId { get; set; }
        [Column("sub_total", TypeName = "decimal(18, 0)")]
        public decimal? SubTotal { get; set; }
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
