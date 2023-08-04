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
        public List<VMListMenu> ListChild { get; set; }
        
    }
}
