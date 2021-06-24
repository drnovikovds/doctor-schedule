using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorScheduleWebAPI.Repositories
{
    public class CityRepository
    {
        private readonly Dictionary<string,string> _cityRepository; 
        public CityRepository ()
        {
            _cityRepository = new Dictionary<string, string>();
        }


    }
}
