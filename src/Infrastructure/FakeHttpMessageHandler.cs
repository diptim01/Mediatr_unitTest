using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var actual = new List<Lead>
            {
                new()
                {
                    FirstName = "Bey",
                    LastName = "Allen",
                    PhoneNumber = "+14155550444",
                    Project = "Construction",
                    PropertyType  = PropertyType.Condo,
                    StartDate = "1/8/2022"
                },
                new()
                {
                    FirstName = "Shawn",
                    LastName = "Brad",
                    PhoneNumber = "+14155550777",
                    Project = "MarlitePanels-(FED)",
                    PropertyType  = PropertyType.Trailer,
                    StartDate = "9/10/2022"
                },
                new()
                {
                    FirstName = "Bob",
                    LastName = "James",
                    PhoneNumber = "+14155550132",
                    Project = "Industrial",
                    PropertyType  = PropertyType.House,
                    StartDate = "3/11/2022"
                },
              
            };

            string leads = JsonConvert.SerializeObject(actual);
            
            return Task.FromResult(new HttpResponseMessage{
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(leads ,Encoding.UTF8, "application/json"),
            });
        }

        public class ErrorHttpMessageHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {

                string leads = JsonConvert.SerializeObject(new Lead());

                return Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(leads, Encoding.UTF8, "application/json"),
                });
            }
        }
    }
}