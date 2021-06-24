using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorScheduleWebAPI.Models
{
    class Patient
    {
        public static int count = 1;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public Patient (string firstName, string lastName, string phoneNumber)
        {
            Id = count;
            count++;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

    }
}
