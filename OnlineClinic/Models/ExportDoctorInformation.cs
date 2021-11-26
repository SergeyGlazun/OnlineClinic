using OnlineClinic.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class ExportDoctorInformation
    {
        public int IdDoctor{ get; set; }
        public Dictionary<string, string> timeReceipt { get; set; }
        public ClinicContext clinicContext { get; set; }
        public int dayRegistration { get; set; }
        public List<string> Time { get; set; }
    }
}
