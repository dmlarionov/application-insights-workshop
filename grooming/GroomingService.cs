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
        private readonly string[] _groomingStyles = { "Neaten", "The Lamb Cut", "The Lion Cut", "The Puppy Cut", "The Schnauzer Cut", "The Teddy Bear Cut", "The Practical Top-Knot" };

        public GroomingService(ILogger<GroomingService> logger) => _logger = logger;

        public Task Groom(Pet pet)
        {
            // take random vaccine
            var style = _groomingStyles[_rnd.Next(8)];
            return Task.CompletedTask;
        }
    }
}
