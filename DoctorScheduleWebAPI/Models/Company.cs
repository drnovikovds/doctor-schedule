using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorScheduleWebAPI.Models
{
    public class Company
    {
        public static int count = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public Company (string name)
        {
            Id = count;
            count++;
            Name = name;
        }
    }
}
