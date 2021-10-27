using dog.Models;
using dog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace dog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly DogService _service;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<DogController> _logger;

        public DogController(DogService service, IHttpClientFactory clientFactory, ILogger<DogController> logger)
        {
            _service = service;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<Dog>> Get() => _service.GetAll();

        [HttpPost]
        public async Task<IActionResult> PostDog(Dog dog)
        {
            _service.Add(dog);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
