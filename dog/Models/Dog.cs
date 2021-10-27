using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dog.Models
{
    public record Dog
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public bool? IsVaccinated { get; set; }

        public Dog() { }

        public Dog(string name, bool isVaccinated = false)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsVaccinated = isVaccinated;
        }
    }
}
