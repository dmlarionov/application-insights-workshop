using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dog
{
    public class VaccinationException : Exception
    {
        public VaccinationException(string message) : base(message) { }
    }
}
