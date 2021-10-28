using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apigw
{
    public class PetException : Exception
    {
        public PetException(string message) : base(message) { }
    }
}
