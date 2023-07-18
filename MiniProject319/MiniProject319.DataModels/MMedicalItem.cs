using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("m_medical_item")]
    public partial class MMedicalItem
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("medical_facility_category_id")]
        public long? MedicalFacilityCategoryId { get; set; }
        [Column("composition", TypeName = "text")]
        public string? Composition { get; set; }
        [Column("medical_item_segmentation_id")]
        public long? MedicalItemSegmentationId { get; set; }
        [Column("manufacturer")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Manufacturer { get; set; }
        [Column("indication", TypeName = "text")]
        public string? Indication { get; set; }
        [Column("dosage", TypeName = "text")]
        public string? Dosage { get; set; }
        [Column("directions", TypeName = "text")]
        public string? Directions { get; set; }
        [Column("contraindication", TypeName = "text")]
        public string? Contraindication { get; set; }
        [Column("caution", TypeName = "text")]
        public string? Caution { get; set; }
        [Column("packaging")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Packaging { get; set; }
        [Column("price_max")]
        public long? PriceMax { get; set; }
        [Column("price_min")]
        public long? PriceMin { get; set; }
        [Column("image")]
        public byte[]? Image { get; set; }
        [Column("image_path")]
        [StringLength(100)]
        [Unicode(false)]
        public string? ImagePath { get; set; }
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
