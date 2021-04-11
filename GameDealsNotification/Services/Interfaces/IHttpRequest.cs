using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDealsNotification.Services.Interfaces
{
    public interface IHttpRequest
    {
        Task<string> GetRequestAsync(string url, Dictionary<string, string> queries);
    }
}
