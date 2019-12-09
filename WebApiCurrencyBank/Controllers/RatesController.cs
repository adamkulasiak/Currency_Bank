using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApiCurrencyBank.Interfaces;

namespace WebApiCurrencyBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatesController : ControllerBase
    {
        private readonly IExchangeRate _rateRepo;
        public RatesController(IExchangeRate rateRepo)
        {
            _rateRepo = rateRepo;
        }

        [HttpGet]
        public IActionResult GetRate([FromQuery] Currency source, Currency dest)
        {
            if (source == dest)
                return BadRequest();

            var result = _rateRepo.ChangeMoney(source, dest);
            return Ok(result.Result);
        }
    }
}