using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("m_user")]
    public partial class MUser
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("biodata_id")]
        public long? BiodataId { get; set; }
        [Column("role_id")]
        public long? RoleId { get; set; }
        [Column("email")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Password { get; set; }
        [Column("login_attemp")]
        public int? LoginAttemp { get; set; }
        [Column("is_looked")]
        public bool? IsLooked { get; set; }
        [Column("last_login", TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }
    }
}
