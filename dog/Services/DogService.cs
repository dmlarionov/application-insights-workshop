using dog.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dog.Services
{
    public class DogService
    {
        private ConcurrentQueue<Dog> _dogs = new ConcurrentQueue<Dog>();
        private readonly Random _rnd = new Random();
        private readonly string[] _dogNames = { "Rufus", "Bear", "Dakota", "Fido", "Vanya", "Samuel", "Koani", "Volodya" };
        private readonly ILogger<DogService> _logger;
        private readonly VaccinationService _vaccination;

        public DogService(ILogger<DogService> logger, VaccinationService vaccination)
        {
            _logger = logger;
            _vaccination = vaccination;
        }

        public Task<List<Dog>> GetAll() => Task.FromResult(_dogs.ToList());

        public Task<Dog> Get(Guid id) => Task.FromResult(_dogs.Where(c => c.Id == id).FirstOrDefault());

        public async Task<(bool, Guid?)> Add(Dog dog)
        {
            (bool, Guid?) result;
            if (dog.Id == null)
                throw new Exception("Every dog must have an identifier!");
            if (_dogs.Where(c => c.Id == dog.Id).Any())
            {
                _logger.LogCritical("Dog conflict detected!");
                result = (false, null);
            }
            if (dog.Name == null)
            {
                dog.Name = _dogNames[_rnd.Next(_dogNames.Length)];
                _logger.LogDebug($"The dog {dog.Name} is added!");
            }
            if (dog.IsVaccinated != true)
                await _vaccination.Vaccinate(dog);
            _dogs.Enqueue(dog);
            result = (true, dog.Id);

            return result;
        }

        //public Task Delete(Guid id)
        //{
        //    _dogs = _dogs.Where(c => c.Id != id).ToList();
        //    return Task.CompletedTask;
        //}
    }
}
