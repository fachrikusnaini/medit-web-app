using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMItemMedical
    {
        public string? NameItem { get; set; }
        public long? MedicalItemCategoryId { get; set; }
        public string? NameItemCategory { get; set; }
        public long? PriceMax { get; set; }
        public long? PriceMin { get; set; }

    }
}
