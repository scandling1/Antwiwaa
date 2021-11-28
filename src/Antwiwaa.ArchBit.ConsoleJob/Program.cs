using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Antwiwaa.ArchBit.ConsoleJob
{
    internal static class Program
    {
        private const string ClientId = "JOBS";
        private const string ClientSecret = "98e0739d-9c90-0966-15dc-ba2b92e1590c";
        private const string BaseUrl = "https://identity.ktu.edu.gh/connect/token";

        private static string _accessToken;

        private static void Main(string[] args)
        {
            Console.WriteLine("Starting ...");
            RunJob().Wait();
        }

        private static async Task RunJob()
        {
            // Get the Access Token.
            _accessToken = await GetAccessToken();
            Console.WriteLine(_accessToken != null ? "Got Token" : "No Token found");

            // Run Job
            Console.WriteLine();
            Console.WriteLine("------ Running Job ------");

            using var client = new HttpClient();

            // client.BaseAddress = new Uri("https://ktuconnectapi.ktu.edu.gh/api/Jobs/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);


            var createStudentResponse = await client.GetAsync("ConvertApplicantToStudent");

            if (createStudentResponse.IsSuccessStatusCode)
            {
                Console.WriteLine();
                Console.WriteLine(createStudentResponse.Content.ReadAsStringAsync().Result);

                var generateIndexNumberResponse = await client.GetAsync("GenerateIndexNumber");

                if (generateIndexNumberResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine();
                    Console.WriteLine(generateIndexNumberResponse.Content.ReadAsStringAsync().Result);

                    var billFreshersResponse = await client.GetAsync("BillFreshers");

                    if (billFreshersResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine();
                        Console.WriteLine(billFreshersResponse.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            else
            {
                Console.WriteLine("Internal server Error");
            }
        }

        private static async Task<string> GetAccessToken()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            // We want the response to be JSON.
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Build up the data to POST.
            var postData = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials"),
                new("client_id", ClientId),
                new("client_secret", ClientSecret)
            };

            var content = new FormUrlEncodedContent(postData);

            // Post to the Server and parse the response.
            var response = await client.PostAsync("Token", content);
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject(jsonString);

            // return the Access Token.
            return ((dynamic)responseData)?.access_token;
        }
    }
}