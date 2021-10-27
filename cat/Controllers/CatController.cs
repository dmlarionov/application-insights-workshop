using cat.Models;
using cat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace cat.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class CatController : ControllerBase
    //{
    //    private readonly CatService _service;
    //    private readonly IHttpClientFactory _clientFactory;
    //    private readonly ILogger<CatController> _logger;

    //    public CatController(CatService service, IHttpClientFactory clientFactory, ILogger<CatController> logger)
    //    {
    //        _service = service;
    //        _clientFactory = clientFactory;
    //        _logger = logger;
    //    }

    //    [HttpGet]
    //    public Task<List<Cat>> Get() => _service.GetAll();

    //    [HttpPost]
    //    public IActionResult PostCat(Cat cat)
    //    {
    //        _service.Add(cat);
    //        return StatusCode(StatusCodes.Status201Created);
    //    }
    //}
}
