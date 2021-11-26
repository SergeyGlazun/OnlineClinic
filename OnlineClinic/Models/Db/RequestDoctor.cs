using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class RequestDoctor
    {
        public int Id { get; set; }
        public int Data { get; set; } 
        public string IdPatient { get; set; }
        public string SurnamePatient { get; set; }
        public int IdDoctor { get; set; }
        public string SurnameDoctor { get; set; }
        public string TimeReceipt { get; set; }
        public string OfficeNumber { get; set; }
    }
}
