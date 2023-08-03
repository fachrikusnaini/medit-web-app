using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject319.ViewModels
{
    public class VMKonsultasi
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? CustomerChatId { get; set; }
        public string? ChatContent { get; set; }

    }
}
