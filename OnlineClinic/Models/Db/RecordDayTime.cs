using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models.Db
{
    public class RecordDayTime
    {   
            public int Id { get; set; }
            public string TimeReceipt { get; set; }
            public  int Day  { get; set; } 
            public int DoctorId { get; set; }           
            public DateTime DayAppeal { get; set; }
    }
}
