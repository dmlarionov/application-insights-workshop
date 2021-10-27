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
        public async Task<IActionResult> GenPets([FromBody] GenPetsDto val)
        {
            //var client = _clientFactory.CreateClient("pet");
            //var result = await client.PostAsJsonAsync("api/pet/genpets", val);
            //if (!result.IsSuccessStatusCode)
            //{
            //    _logger.LogError("Something went wrong!");
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            return Ok();
        }
    }
}