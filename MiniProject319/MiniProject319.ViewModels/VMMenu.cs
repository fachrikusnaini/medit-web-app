using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMMenu
    {
     
        public long Id { get; set; }
        public long? MenuId { get; set; }
        public string? Name { get; set; }
        public string? MenuName { get; set; }
        public string? Url { get; set; }
        public long? RoleId { get; set; }
        public string? RoleName { get; set; }
        public long? ParentId { get; set; }
        
        public string? BigIcon { get; set; }
        
        public string? SmallIcon { get; set; }
        
        public long CreatedBy { get; set; }
       
        public DateTime CreatedOn { get; set; }
       
        public long? ModifiedBy { get; set; }
       
        public DateTime? ModifiedOn { get; set; }
      
        public long? DeletedBy { get; set; }
    
        public DateTime? DeletedOn { get; set; }
       
        public bool IsDelete { get; set; }
    }
}
