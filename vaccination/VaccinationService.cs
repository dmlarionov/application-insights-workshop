using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vaccination.Models;

namespace vaccination
{
    public class VaccinationService
    {

        ILogger<VaccinationService> _logger;
        private readonly Random _rnd = new Random();
        private readonly string[] _certifiedVaccines = { "astrazeneca", "sputnik", "pfizer", "stackoverflow" };

        public VaccinationService(ILogger<VaccinationService> logger) => _logger = logger;

        public Task<VaccinatedPet> Vaccinate(Pet pet)
        {
            // take random vaccine
            var vaccine = _certifiedVaccines[_rnd.Next(5)];
            return Task.Run(() => new VaccinatedPet(pet.id, vaccine));
        }
    }
}
