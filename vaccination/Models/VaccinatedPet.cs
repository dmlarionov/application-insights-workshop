using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaccination.Models
{
    public record VaccinatedPet (Guid id, string vaccineName);
}
