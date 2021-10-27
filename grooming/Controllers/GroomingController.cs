using grooming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grooming.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class GroomingController : ControllerBase
    {
        private readonly ILogger<GroomingController> _logger;
        private readonly GroomingService _service;

        public GroomingController(ILogger<GroomingController> logger, GroomingService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public Task Groom(Pet pet) => _service.Groom(pet);
    }
}
