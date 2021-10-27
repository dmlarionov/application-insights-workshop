using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pet.Models
{
    public record Pet
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public bool? IsVaccinated { get; set; }
    }
}
