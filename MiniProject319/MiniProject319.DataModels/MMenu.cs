using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("m_menu")]
    public partial class MMenu
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("url")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Url { get; set; }
        [Column("parent_id")]
        public long? ParentId { get; set; }
        [Column("big_icon")]
        [StringLength(100)]
        [Unicode(false)]
        public string? BigIcon { get; set; }
        [Column("small_icon")]
        [StringLength(100)]
        [Unicode(false)]
        public string? SmallIcon { get; set; }
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
