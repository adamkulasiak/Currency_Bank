using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// Akcja do pobierania danych z nbp
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRates()
        {
            var eur = await _rateRepo.GetEURRateFromNbp();
            var usd = await _rateRepo.GetUSDRateFromNbp();
            var gbp = await _rateRepo.GetGBPRateFromNbp();
            string result = usd + "\n" + eur + "\n" + gbp;
            return Ok(result);
        }
    }
}