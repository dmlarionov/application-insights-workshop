using dog.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace dog.Services
{
    public class VaccinationService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<VaccinationService> _logger;

        public VaccinationService(IHttpClientFactory clientFactory, ILogger<VaccinationService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task Vaccinate(Dog dog)
        {
            _logger.LogInformation("Vaccinating a dog");
            var client = _clientFactory.CreateClient("vaccination");
            var response = client.PostAsJsonAsync("api/vaccinate", dog);
            if ((await response).StatusCode != System.Net.HttpStatusCode.OK)
                throw new VaccinationException("We can't afford unvaccinated pets");
        }
    }
}
