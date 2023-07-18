using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMTblUsers
    {
        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public long? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? LoginAttempt { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LastLogin { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}
