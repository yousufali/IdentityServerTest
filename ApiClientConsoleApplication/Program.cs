using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ApiClientConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var disco =  DiscoveryClient.GetAsync("http://localhost:5000").Result;
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            //var tokenResponse =  tokenClient.RequestClientCredentialsAsync("api1").Result;
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("test", "test").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);


            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response =  client.GetAsync("http://localhost:54169/identity").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }

            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(JArray.Parse(content));
        }
    }
}
