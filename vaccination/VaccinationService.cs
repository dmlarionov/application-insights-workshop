using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vaccination.Models;

namespace vaccination
{
    public class VaccinationService
    {

        ILogger<VaccinationService> _logger;
        private readonly Random _rnd = new Random();
        private readonly string[] _certifiedVaccines = {
            "Adenovirus Type 4",
            "Adenovirus Type 7",
            "BioThrax",
            "Vaxchora",
            "Daptacel",
            "Infanrix",
            "Sanofi",
            "ActHIB",
            "Hiberix",
            "PedvaxHIB",
            "Havrix",
            "Vaqta",
            "Engerix-B",
            "Recombivax HB",
            "Heplisav-B",
            "Shingrix",
            "Gardasil",
            "Afluria",
            "Fluad",
            "Fluarix",
            "Flublok",
            "Flucelvax",
            "FluLaval",
            "FluMist",
            "Fluzone",
            "Fluzone High-Dose",
            "Ixiaro",
            "M-M-R II",
            "Menactra",
            "Menveo",
            "Trumenba",
            "Bexsero",
            "Pneumovax 23",
            "Prevnar 13",
            "Ipol",
            "Imovax Rabies",
            "RabAvert",
            "RotaTeq",
            "Rotarix",
            "Tenivac",
            "Tetanus",
            "Boostrix",
            "Adacel",
            "Typhim Vi",
            "Vivotif",
            "Varivax",
            "ACAM2000",
            "YF-Vax",
            "Kinrix",
            "Quadracel",
            "Pediarix",
            "Pentacel",
            "Twinrix",
            "ProQuad",
            "MMRV",
            "HepA",
            "HepB",
            "HepA-HepB",
            "DTaP-IPV/Hib",
            "DTaP-HepB-IPV",
            "DTaP-IPV",
            "DTaP-IPV",
            "YF",
            "VAR",
            "Tdap",
            "Tdap",
            "Td",
            "RV1",
            "RV5",
            "IPV",
            "PCV13",
            "PPSV23",
            "MenB-4C",
            "MenB-FHbp",
            "MCV4 MenACWY-CRM",
            "MCV4 MenACWY-D",
            "MMR",
            "JE",
            "IIV3",
            "IIV4",
            "LAIV4",
            "IIV4",
            "ccIIV4",
            "RIV4",
            "IIV4",
            "IIV3",
            "9vHPV",
            "RZV",
            "HepB",
            "Hib (PRP-OMP)",
            "Hib (PRP-T)",
            "Hib (PRP-T)",
            "DT",
            "DTaP",
            "AVA",
            "Barr",
            "PaxVax",
            "Sanofi"
        };

        public VaccinationService(ILogger<VaccinationService> logger) => _logger = logger;

        public Task<VaccinatedPet> Vaccinate(Pet pet)
        {
            // take random vaccine
            var vaccine = _certifiedVaccines[_rnd.Next(100)];
            return Task.Run(() => new VaccinatedPet(pet.id, vaccine));
        }
    }
}
