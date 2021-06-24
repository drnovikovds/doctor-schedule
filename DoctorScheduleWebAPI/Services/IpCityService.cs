using System;
using System.Threading.Tasks;
using DoctorScheduleWebAPI.Repositories;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using DoctorScheduleWebAPI.Models;
using DoctorScheduleWebAPI.Models.UserDto;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;

namespace DoctorScheduleWebAPI.Services
{
    public class IpCityService
    {
        private readonly JsonSerializer _jsonSerializer = new JsonSerializer
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy
                {
                    ProcessDictionaryKeys = true,
                    OverrideSpecifiedNames = true
                }
            }
        };
        private readonly CityRepository _cityRepository;

        public IpCityService(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<City> GetCityByIpAsync (IpToGetCityModel ipToGetCityModel)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://suggestions.dadata.ru/suggestions/api/4_1/rs/iplocate/address"),
                Content = Serialize<IpToGetCityModel>(ipToGetCityModel)
            };

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token","7a094c9cc14d12536385f2ee62ae14a3365c340d");
            var httpResponse = await httpClient.SendAsync(requestMessage);
            if (!httpResponse.IsSuccessStatusCode)
                return null;
            
            using (var jsonStream = new MemoryStream(await httpResponse.Content.ReadAsByteArrayAsync(), false))
            using (var streamReader = new StreamReader(jsonStream))
            using (var jsonReader = new JsonTextReader(streamReader))
                return _jsonSerializer.Deserialize<City>(jsonReader);
        }

        private HttpContent Serialize<T>(T data)
        {
            var stream = new MemoryStream();
            using (var streamWriter = new StreamWriter(stream, encoding: new UTF8Encoding(false), bufferSize: 1024, leaveOpen: true))
            using (var writer = new JsonTextWriter(streamWriter))
            {
                _jsonSerializer.Serialize(writer, data);
                writer.Flush();
            }

            stream.Seek(0, SeekOrigin.Begin);
            var httpContent = new StreamContent(stream);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            return httpContent;
        }
    }
}
