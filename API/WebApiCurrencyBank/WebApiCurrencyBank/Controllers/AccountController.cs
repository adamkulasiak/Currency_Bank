using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCurrencyBank.Interfaces;

namespace WebApiCurrencyBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountRepo;
        public AccountController(IAccount accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _accountRepo.Create(currentUserId, Currency.PLN);

            if (result is null)
                return BadRequest();

            return Created("", result);
        }
    }
}