using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using vaccination.Models;

namespace vaccination.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class VaccinationController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<VaccinationController> _logger;
        private readonly VaccinationService _service;

        public VaccinationController(IHttpClientFactory clientFactory, ILogger<VaccinationController> logger, VaccinationService service)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<VaccinatedPet> Vaccinate(Pet pet) => await _service.Vaccinate(pet);
    }
}
