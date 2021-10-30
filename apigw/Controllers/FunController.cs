using apigw.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public IActionResult GenPets([FromBody] GenPetRequest request)
        {
            var requests = new List<Task<HttpResponseMessage>>();
            int numOfPets = request.batchSize;
            var client = _clientFactory.CreateClient("pet");
            Func<Task<HttpResponseMessage>> method = (request.kindOfPet == "dog") ?
                () => client.PostAsJsonAsync("api/pets/gendog", new { }) :
                () => client.PostAsJsonAsync("api/pets/gencat", new { });
            _logger.LogInformation($"Generating {numOfPets} pets");
            for(int i = 0; i < numOfPets; i++)
                requests.Add(method());
            requests.ForEach(r => {
                if (!(r.Result).IsSuccessStatusCode)
                    throw new PetException("Can't generate a pet");
            });
            return Ok();
        }

        [HttpPost("pets/groom")]
        public async Task<IActionResult> GroomPets()
        {
            _logger.LogInformation("Grooming all the pets");
            var client = _clientFactory.CreateClient("pet");
            var result = await client.PostAsJsonAsync("api/pets/groom", new { });
            return new ObjectResult(new { }) { StatusCode = ((int)result.StatusCode) };
        }

        [HttpPost("pets/play")]
        public async Task<IActionResult> PlayPets()
        {
            _logger.LogInformation("Going to play with the pets");
            var client = _clientFactory.CreateClient("pet");
            var result = await client.PostAsJsonAsync("api/pets/play", new { }).ConfigureAwait(true);
            return new ObjectResult(new { }) { StatusCode = ((int)result.StatusCode) };
        }
    }
}