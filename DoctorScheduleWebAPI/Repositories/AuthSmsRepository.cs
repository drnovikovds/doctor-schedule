using System;
using System.Collections.Generic;

namespace DoctorScheduleWebAPI.Repositories
{
    public class AuthSmsRepository
    {
        private readonly Dictionary<string, string> _authSmsDict;
        private readonly Random _random;

        public AuthSmsRepository()
        {
            _authSmsDict = new Dictionary<string, string>();
            _random = new Random();
        }

        public string AddNumberAndCode(string number)
        {
            if (!_authSmsDict.ContainsKey(number))
            {
                _authSmsDict.Add(number, null);
            }

            var smsCode = CreateSmsCode();
            _authSmsDict[number] = smsCode;

            return smsCode;
        }

        public bool IsValidCode(string number, string smsCode)
        {
            return _authSmsDict.ContainsKey(number) 
                && string.Equals(_authSmsDict[number], smsCode);
        }

        private string CreateSmsCode()
        {
            return _random.Next(1000, 9999).ToString();
        }
    }
}
