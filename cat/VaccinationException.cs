using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cat
{
    public class VaccinationException : Exception
    {
        public VaccinationException(string message) : base(message) { }
    }
}
