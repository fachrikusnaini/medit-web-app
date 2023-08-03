using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMCustomerRelation
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public string? Fullname { get; set; }
        public long CreatedBy { get; set; }
       
        public DateTime CreatedOn { get; set; }
       
        public long? ModifiedBy { get; set; }
        
        public DateTime? ModifiedOn { get; set; }
       
        public long? DeletedBy { get; set; }
       
        public DateTime? DeletedOn { get; set; }
       
        public bool IsDelete { get; set; }
    }
}
