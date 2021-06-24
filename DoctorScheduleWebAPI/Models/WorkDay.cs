using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorScheduleWebAPI.Models
{
    public class WorkDay
    {
        public readonly double timeForPerson = 20; 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
