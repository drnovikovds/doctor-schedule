using System;
using System.Threading.Tasks;
using System.Net;

namespace DoctorScheduleWebAPI.Services
{
    public class SmsSenderService
    {
        public async Task SendSmsAsync(string number, string message)
        {
            var url = $"https://sms.ru/sms/send?api_id=9F60775E-7F05-97D4-BAD8-55BF43A79EA4&to={number}&msg={message}";
            await HttpWebRequest.Create(url).GetResponseAsync();
        }
        public async Task<bool> SendSmsAsync(string message)
        {
            try
            {
                var url = $"https://sms.ru/sms/send?api_id=9F60775E-7F05-97D4-BAD8-55BF43A79EA4&to=79214411198&msg={message}";
                await HttpWebRequest.Create(url).GetResponseAsync();

                return true;
            }           
            
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                // TODO Log errors in file;

                return false;
            }
        }
    }
}
