using grooming.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grooming
{
    public class GroomingService
    {
        ILogger<GroomingService> _logger;
        private readonly Random _rnd = new Random();

        public GroomingService(ILogger<GroomingService> logger) => _logger = logger;

        public void Groom(Pet pet)
        {
            // take random offer
            var offer = GroomingOffer.Current[_rnd.Next(GroomingOffer.Current.Length)];
            _logger.LogInformation($"{pet.Name} was groomed by {offer.Hairdresser.Name}");
        }
    }
}
