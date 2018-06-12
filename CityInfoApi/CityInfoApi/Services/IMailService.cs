using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoApi.Services
{
   public interface IMailService
    {
        void Send(string subject, string message);
    }
}
