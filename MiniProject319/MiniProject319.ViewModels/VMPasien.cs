using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMPasien
    {
 
        public long Id { get; set; }

        public long? ParentBiodataId { get; set; }
    
        public long? CustomerId { get; set; }
      
        public long? CustomerRelationId { get; set; }

        public string? Fullname { get; set; }

        public DateTime? Dob { get; set; }
   
        public string? Gender { get; set; }

        public long? BloodGroupId { get; set; }
   
        public string? RhesusType { get; set; }
      
        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public string? Name { get; set; }

        public long CreatedBy { get; set; }
      
        public DateTime CreatedOn { get; set; }
       
        public long? ModifiedBy { get; set; }
 
        public DateTime? ModifiedOn { get; set; }
     
        public long? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDelete { get; set; }
    }
}
