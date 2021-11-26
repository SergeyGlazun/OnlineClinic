using OnlineClinic.Models.AuthorizationAuthentication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class ExportStatictic
    {
        public int CountUsers { get; set; }
        public int CountDoctors { get; set; }
        public int CountRequest { get; set; }
        public RegisterModel model { get; set; }
    }
}
