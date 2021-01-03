using System.IO;
using System.Net;

namespace Application.ApplicationLayer.Common
{
    public class WebCaller
    {
        public static string Call(string url)
        {
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    return content;
                }
            }
        }
    }
}
