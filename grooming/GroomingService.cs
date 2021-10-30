using grooming.Models;
using Microsoft.ApplicationInsights;
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
        private readonly TelemetryClient _telemetry;
        private readonly Random _rnd = new Random();

        public GroomingService(ILogger<GroomingService> logger, TelemetryClient telemetry)
        {
            _telemetry = telemetry;
            _logger = logger;
        }

        public void Groom(Pet pet)
        {
            // take random offer
            var offer = GroomingOffer.Current[_rnd.Next(GroomingOffer.Current.Length)];
            _logger.LogDebug($"The {pet.Kind} {pet.Name} was groomed by {offer.Hairdresser.Name}");
            _telemetry.GetMetric("PetsGroomed", "Kind", "Name")
                .TrackValue(1, pet.Kind, pet.Name);

        }
    }
}
