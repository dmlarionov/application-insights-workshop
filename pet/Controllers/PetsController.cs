using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        [Authorize]
        public IActionResult Play()
        {
            _logger.LogWarning("Attempt to play with the pets!!");
            return Ok();
        }
    }
}
