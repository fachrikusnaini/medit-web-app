using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMListMenu
    {
        public long? MenuId { get; set; }
        public string? MenuName { get; set; }
        public long? RoleId { get; set; }
        public string? RoleName { get; set; }
        public long? Parent_Id { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        //public List<VMListMenu> listMenus { get; set; }
    }
}
