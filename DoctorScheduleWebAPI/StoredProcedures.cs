using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorScheduleWebAPI
{
    public static class StoredProcedures
    {
        public static string CreateDoctor = "p_Doctor_Insert";
        public static string GetDoctorById = "p_Doctor_GetById";
        public static string UpdateDoctor = "p_Doctor_Update";
        public static string DeleteDoctorById = "p_Doctor_DeleteById";
    }
}
