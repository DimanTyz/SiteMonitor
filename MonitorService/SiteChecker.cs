using System.Net;
using System.Net.Http;

namespace MonitorService
{
    class SiteChecker
    {
        public static HttpStatusCode Check(string site)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(site).Result;

                return response.StatusCode;
            }
        }
    }
}
