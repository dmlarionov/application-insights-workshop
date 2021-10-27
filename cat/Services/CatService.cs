using cat.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cat.Services
{
    public class CatService
    {
        private List<Cat> _cats = new List<Cat>();
        private readonly Random _rnd = new Random();
        private readonly string[] _catNames = { "Prince", "Yiska", "Bella", "Charlie", "Lucy", "Leo", "Milo", "Jack" };
        private readonly ILogger<CatService> _logger;
        private readonly VaccinationService _vaccination;

        public CatService(ILogger<CatService> logger, VaccinationService vaccination)
        {
            _logger = logger;
            _vaccination = vaccination;
        }

        public Task<List<Cat>> GetAll() => Task.FromResult(_cats);

        public Task<Cat> Get(Guid id) => Task.FromResult(_cats.Where(c => c.Id == id).FirstOrDefault());

        public async Task<(bool, Guid?)> Add(Cat cat)
        {
            (bool, Guid?) result;
            if (cat.Id == null)
                throw new Exception("Every cat must have an identifier!");
            if (_cats.Where(c => c.Id == cat.Id).Any())
            {
                _logger.LogCritical("Cat conflict detected!");
                result = (false, null);
            }
            if (cat.Name == null)
            {
                _logger.LogInformation("Attempt to add a cat without name!");
                cat.Name = _catNames[_rnd.Next(_catNames.Length)];
            }
            if (cat.IsVaccinated != true)
                await _vaccination.Vaccinate(cat);
            _cats.Add(cat);
            result = (true, cat.Id);

            return result;
        }

        public Task Delete(Guid id)
        {
            _cats = _cats.Where(c => c.Id != id).ToList();
            return Task.CompletedTask;
        }
    }
}
