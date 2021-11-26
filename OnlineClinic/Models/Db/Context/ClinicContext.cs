using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models.Db
{
    public class ClinicContext: DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<RequestDoctor> Requests { get; set; }
        public DbSet<RecordDayTime> RecordTimes { get; set; }

        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

    }
}
