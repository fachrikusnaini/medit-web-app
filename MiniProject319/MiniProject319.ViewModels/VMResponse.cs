using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMResponse
    {
        public VMResponse()
        {
            Success = true;
        }
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Entity { get; set; }
    }
}
