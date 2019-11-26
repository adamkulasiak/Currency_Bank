using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCurrencyBank.Dtos;
using WebApiCurrencyBank.Interfaces;

namespace WebApiCurrencyBank.Controllers
{
    /// <summary>
    /// Kontroler do zarzadzania kontami
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountRepo;
        private readonly IMapper _mapper;
        public AccountController(IAccount accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Akcja do dodawania konta dla uzytkownika ktory wywola ta metode
        /// </summary>
        /// <param name="accountToCreateDto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create(AccountToCreateDto accountToCreateDto)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var account = _mapper.Map<Account>(accountToCreateDto);
            var result = await _accountRepo.Create(currentUserId, account.Currency);

            if (result is null)
                return BadRequest();

            return Created("", result);
        }

        [HttpPut("cashin")]
        public async Task<IActionResult> CashIn([FromQuery]int accountId, decimal ammount)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var account = await _accountRepo.CashIn(currentUserId, accountId, ammount);

            if (account is null)
                return BadRequest();

            return Ok(account);
        }
    }
}