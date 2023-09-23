using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CTeleport.Distance.Api
{
    public sealed class PlacesClient : IDisposable
    {
        private readonly HttpClient client;

        public PlacesClient()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://places-dev.cteleport.com"),
                DefaultRequestVersion = new Version(1, 1)
            };
        }

        public async Task<Airport> GetAirport(IATA code) 
        {
            try
            {
                return await client.GetFromJsonAsync<Airport>($"airports/{code}");
            }
            catch
            {
                // logging or any other handlind could be here
                return null;
            }
        } 

        public void Dispose() => client.Dispose();
    }
}
