using Contact.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("PersistenceApi");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactRequest contact)
        {
            var response = await _httpClient.PostAsJsonAsync("api/contact", contact);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
   
   
}
