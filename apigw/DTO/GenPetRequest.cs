﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apigw.DTO
{
    public record GenPetRequest(string kindOfPet, int batchSize);
}
