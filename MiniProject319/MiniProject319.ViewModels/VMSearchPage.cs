using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMSearchPage
    {
        public string? CodeTransaction { get; set; }
        public string? NameProduct { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public int? CurrentPageSize { get; set; }
    }
}
