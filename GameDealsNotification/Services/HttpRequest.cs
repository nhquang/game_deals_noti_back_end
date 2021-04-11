using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GameDealsNotification.Services.Interfaces;
using Newtonsoft.Json;

namespace GameDealsNotification.Services
{
    public class HttpRequest : IHttpRequest
    {
        public async Task<string> GetRequestAsync(string url, Dictionary<string,string> queries = null)
        {
            HttpClient client = new HttpClient();
            if(queries != null)
            {
                url += "?";
                foreach (var item in queries) url += $"{item.Key}={item.Value}&";
            }
            var result = await client.GetStringAsync(url.TrimEnd('&'));
            return result;
        }
    }
}
