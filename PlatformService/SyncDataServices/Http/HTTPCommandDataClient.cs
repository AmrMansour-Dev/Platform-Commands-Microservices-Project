using PlatformService.DTOs;
using System.Drawing.Text;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HTTPCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HTTPCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDTO plat)
        {
            var httpcontent = new StringContent(JsonSerializer.Serialize(plat),Encoding.UTF8,"application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpcontent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST To CommandService is OK");
            }
            else
            {
                Console.WriteLine("--> Sync POST To CommandService is NOT OK!");

            }
        }
    }
}
