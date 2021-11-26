using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models.Db.Inittializer
{
    public partial  class DbInitializer
    {
        public static async Task InitializeAsync(ClinicContext clinic)
        {
            if (clinic.Doctors.Count() == 0)
            {
                clinic.Doctors.Add(
                      new Doctor
                      {                       
                          Foto = "/img/foto/врач_1.jfif",
                          Surname = "Мехов",
                          Name = "Дмитрий",
                          Patronymic = "Александрович",
                          Specialty = "врач-хирург",
                          IdRecordDayTime = 1,
                          OfficeNumber = "12",
                         
                      });
                clinic.Doctors.Add(
                new Doctor
                {
                    Foto = "/img/foto/врач_2.jfif",
                    Surname = "Лызиков",
                    Name = "Алексей",
                    Patronymic = "Анатольевич",
                    Specialty = "кардиолог",
                  
                    OfficeNumber = "13",
                  
                });
                clinic.Doctors.Add(
                    new Doctor
                    {
                        Foto = "/img/foto/врач_3.jfif",
                        Surname = "Прокопенко",
                        Name = "Наталья",
                        Patronymic = "Борисовна",
                        Specialty = "врач-акушер-гинеколог",
                     
                        OfficeNumber = "15",
                       
                        
                    });
               clinic.Doctors.Add(
                   new Doctor
                    {
                        Foto = "/img/foto/врач_4.jpg",
                       Surname = "Маслянский",
                       Name = "Борис",
                       Patronymic = "Абович",
                       Specialty = "врач-ангиохирург-флеболог ( специалист по склеротерапии варикознораширенных вен)",                      
                       OfficeNumber = "14",
                      

                   });
                clinic.SaveChanges();
            }
            if (clinic.Patients.Count() == 0)
            {
                clinic.Patients.Add(
                    new Patient
                    {
                        Surname = "Седельник",
                        Name = "Владимир",
                        Patronymic = "Сергеевич",
                        YearOfBirth = "1991",
                        Address = "Гомель",
                        Login = "userS",
                        Password = "qwerty"
                    });
                clinic.SaveChanges();
            }
           
            if (clinic.RecordTimes.Count() == 0)
            {
                DateTime now = DateTime.Now;
                clinic.RecordTimes.Add(
                    new RecordDayTime
                    {
                        Day = now.Day,
                        TimeReceipt = "8:30",
                        DoctorId =1,
                        DayAppeal = DateTime.Now
                    }                
                    );
                clinic.RecordTimes.Add(
                   new RecordDayTime
                   {
                       Day = 14,
                       TimeReceipt = "9:30",
                       DoctorId = 1,
                       DayAppeal = DateTime.Now
                   }
                   );
                clinic.RecordTimes.Add(
                  new RecordDayTime
                  {
                      Day = 14,
                      TimeReceipt = "10:00",
                      DoctorId = 1,
                      DayAppeal = DateTime.Now
                  }
                  );
                clinic.SaveChanges();
            }
        }
    }
}
