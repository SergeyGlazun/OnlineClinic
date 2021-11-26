using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string YearOfBirth { get; set; }
        public string Address { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
