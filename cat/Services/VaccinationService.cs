using cat.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace cat.Services
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

        public async Task Vaccinate(Cat cat)
        {
            _logger.LogInformation("Vaccinating a cat");
            var client = _clientFactory.CreateClient("vaccination");
            var response = client.PostAsJsonAsync("api/vaccinate", cat);
            if ((await response).StatusCode != System.Net.HttpStatusCode.OK)
                throw new VaccinationException("We can't afford unvaccinated pets");
        }
    }
}
