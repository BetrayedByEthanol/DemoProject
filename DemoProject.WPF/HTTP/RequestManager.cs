using Meziantou.Framework.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.WPF.HTTP
{
    class RequestManager
    {
        private static HttpClient client = new()
        {
            BaseAddress = new Uri("http://localhost")
        };

        public static string Get(string path)
        {
            var response = client.GetAsync(path).GetAwaiter().GetResult();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (response.RequestMessage.RequestUri.AbsolutePath is "/OAuth/Authorize")
            {
                var form = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("refresh_token","my token")
                });

                var formResponse = client.PostAsync("/OAuth/Authorize", form).GetAwaiter().GetResult();
                content = formResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (formResponse.RequestMessage.RequestUri.AbsolutePath == "/login") throw new UnauthorizedAccessException();

                Debug.WriteLine(formResponse.RequestMessage.RequestUri.AbsolutePath);

                var jsonStructure = new { access_token = "", refresh_token = "", redirect_uri = "" };
                var data = JsonConvert.DeserializeAnonymousType(content, jsonStructure);
                Debug.WriteLine(data.access_token);

                var cache = MemoryCache.Default;
                cache.Set("accessToken", data.access_token, DateTimeOffset.Now.AddMinutes(5));
                var accessToken = cache.Get("accessToken") as string;

                Debug.WriteLine(accessToken);
                CredentialManager.WriteCredential("DemoProject", "DemoUser", data.refresh_token, CredentialPersistence.LocalMachine);
                var rToken = CredentialManager.ReadCredential("DemoProject").Password;

                var cookie = new Cookie()
                {
                    Domain = "http://localhost",
                    Secure = true,
                    HttpOnly = true,
                    Value = accessToken,
                    Name = "access_token"
                };

                client.DefaultRequestHeaders.Add("Cookie",cookie.ToString() );

                response = client.GetAsync(path).GetAwaiter().GetResult();
                content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            return content;
        }
    }
}
