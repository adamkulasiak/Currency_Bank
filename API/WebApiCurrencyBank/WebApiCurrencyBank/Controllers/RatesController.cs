using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetRates()
        {
            var result = await _rateRepo.GetRatesFromNbp();
            if (result) return Ok();
            else return BadRequest();
        }
    }
}