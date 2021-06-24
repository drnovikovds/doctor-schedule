using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorScheduleWebAPI.Models
{
    public class City
    {
        public Location Location { get; set; }
    }

    public class Location
    {
        public string Value { get; set; }
        public string UnrestrictedValue { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string CountryIsoCode { get; set; }
        public string FederalDistrict { get; set; }
        public string regionFiasId { get; set; }
        public string regionKladrId { get; set; }
        public string regionIsoCode { get; set; }
        public string regionWithType { get; set; }
        public string regionType { get; set; }
        public string regionTypeFull { get; set; }
        public string region { get; set; }
        public string cityFiasId { get; set; }
        public string cityKladrId { get; set; }
        public string cityWithType { get; set; }
        public string cityType { get; set; }
        public string cityTypeFull { get; set; }
        public string city { get; set; }
        public string fiasId { get; set; }
        public string fiasCode { get; set; }
        public string fiasLevel { get; set; }
        public string fiasActuality_state { get; set; }
        public string kladrId { get; set; }
        public string geonameId { get; set; }
        public string capitalMarker { get; set; }
        public string okato { get; set; }
        public string oktmo { get; set; }
        public string tax_office { get; set; }
        public string tax_officeLegal { get; set; }
        public string geoLat { get; set; }
        public string geoLon { get; set; }
        public string qc_geo { get; set; }
    }
}

