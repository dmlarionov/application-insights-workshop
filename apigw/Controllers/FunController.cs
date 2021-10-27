using apigw.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace apigw.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FunController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<FunController> _logger;

        public FunController(IHttpClientFactory clientFactory, ILogger<FunController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpPost("pets/generate")]
        public async Task<IActionResult> GenPets([FromBody] GenPetRequest request)
        {
            Task<HttpResponseMessage> response = null;
            if (request.kindOfPet == "dog")
            {
                _logger.LogInformation("Generating a dog");
                var client = _clientFactory.CreateClient("pet");
                response = client.PostAsJsonAsync("api/pets/gendog", new { });
            }
            else
            {
                _logger.LogInformation("Generating a cat");
                var client = _clientFactory.CreateClient("pet");
                response = client.PostAsJsonAsync("api/pets/gencat", new { });
            }
            return new ObjectResult(new { }) { StatusCode = (int)(await response).StatusCode };
        }

        [HttpPost("pets/groom")]
        public async Task<IActionResult> GroomPets()
        {
            _logger.LogInformation("Grooming the pets");
            var client = _clientFactory.CreateClient("pet");
            var result = await client.PostAsJsonAsync("api/pets/groom", new { });
            return new ObjectResult(new { }) { StatusCode = ((int)result.StatusCode) };
        }

        [HttpPost("pets/play")]
        public async Task<IActionResult> KillPets()
        {
            _logger.LogInformation("Playing the pets");
            var client = _clientFactory.CreateClient("pet");
            var result = await client.PostAsJsonAsync("api/pets/play", new { }).ConfigureAwait(true);
            return new ObjectResult(new { }) { StatusCode = ((int)result.StatusCode) };
        }
    }
}