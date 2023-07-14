using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("t_token")]
    public partial class TToken
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("email")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("user_id")]
        public long? UserId { get; set; }
        [Column("token")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Token { get; set; }
        [Column("expired_on", TypeName = "datetime")]
        public DateTime? ExpiredOn { get; set; }
        [Column("is_expired")]
        public bool? IsExpired { get; set; }
        [Column("used_for")]
        [StringLength(20)]
        [Unicode(false)]
        public string? UsedFor { get; set; }
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
