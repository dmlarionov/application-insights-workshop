using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace pet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<PetsController> _logger;

        public PetsController(IHttpClientFactory clientFactory, ILogger<PetsController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GenDog()
        {
            _logger.LogInformation("Generating a dog");
            var client = _clientFactory.CreateClient("dog");
            var response = client.PostAsJsonAsync("api/dog", new { Id = Guid.NewGuid() });
            return new ObjectResult(new { }) { StatusCode = (int)(await response).StatusCode };
        }

        [HttpPost]
        public async Task<IActionResult> GenCat()
        {
            _logger.LogInformation("Generating a cat");
            var client = _clientFactory.CreateClient("cat");
            var response = client.PostAsJsonAsync("api/cat", new { Id = Guid.NewGuid() });
            return new ObjectResult(new { }) { StatusCode = (int)(await response).StatusCode };
        }

        [HttpPost]
        [Authorize]
        public IActionResult Play()
        {
            _logger.LogWarning("Attempt to play with the pets!!");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Groom()
        {
            _logger.LogInformation("Grooming all the pets");
            var pets = (await _clientFactory.CreateClient("dog").GetFromJsonAsync<List<Pet>>("api/dog"))
                .Select(i => { i.Kind = "dog"; return i; })
                .Concat(await _clientFactory.CreateClient("cat").GetFromJsonAsync<List<Pet>>("api/cat"))
                .Select(i => { i.Kind = "cat"; return i; });
            foreach (var pet in pets)
            {
                var response = await _clientFactory.CreateClient("grooming").PostAsJsonAsync("api/groom", pet);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Weird thing happened!");
            }
            return Ok();
        }
    }
}
