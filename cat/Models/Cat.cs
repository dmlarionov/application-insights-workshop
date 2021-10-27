using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cat.Models
{
    public record Cat
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public bool? IsVaccinated { get; set; }

        public Cat() { }

        public Cat(string name, bool isVaccinated = false)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsVaccinated = isVaccinated;
        }
    }
}
