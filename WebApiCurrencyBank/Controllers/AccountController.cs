using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CurrencyBank.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CurrencyBank.API.Dtos;
using CurrencyBank.API.Interfaces;

namespace CurrencyBank.API.Controllers
{
    /// <summary>
    /// Kontroler do zarzadzania kontami
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IMapper _mapper;
        public AccountController(IAccountRepository accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Akcja do pobierania kont dla zalogowanego uzytkownika
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAccounts")]
        public async Task<IActionResult> GetAccountsForUser()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var accounts = await _accountRepo.GetAccountForUser(currentUserId);
            if (accounts is null)
                return NotFound();
            return Ok(accounts);
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

        /// <summary>
        /// Akcja sluzaca do wplaty pieniedzy na konto
        /// </summary>
        /// <param name="accountId">id konta</param>
        /// <param name="ammount">kwota</param>
        /// <returns></returns>
        [HttpPut("cashin")]
        public async Task<IActionResult> CashIn([FromQuery]int accountId, decimal ammount)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var account = await _accountRepo.CashIn(currentUserId, accountId, ammount);

            if (account is null)
                return BadRequest();

            return Ok(account);
        }

        /// <summary>
        /// Akcja sluzaca do wyplaty pieniedzy z konta
        /// </summary>
        /// <param name="accountId">id konta</param>
        /// <param name="ammount">kwota</param>
        /// <returns></returns>
        [HttpPut("cashout")]
        public async Task<IActionResult> CashOut([FromQuery]int accountId, decimal ammount)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var account = await _accountRepo.CashOut(currentUserId, accountId, ammount);

            if (account is null)
                return BadRequest();

            return Ok(account);
        }

        /// <summary>
        /// Akcja sluzaca do wymiany waluty 
        /// </summary>
        /// <param name="sourceAccountId">id konta z ktorego pobieramy pieniadze do wymiany</param>
        /// <param name="destinationAccountId">id konta docelowego w innej walucie niz zrodlowa</param>
        /// <param name="ammount">kwota</param>
        /// <returns></returns>
        [HttpPut("exchange")]
        public async Task<IActionResult> Exchange([FromQuery]int sourceAccountId, int destinationAccountId, decimal ammount)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var accounts = await _accountRepo.ExchangeMoney(currentUserId, sourceAccountId, destinationAccountId, ammount);

            if (accounts is null)
                return BadRequest();

            return Ok(accounts);
        }

        /// <summary>
        /// Akcja sluzaca do wykonania przelewu
        /// </summary>
        /// <param name="principalAccountId">id konta zleceniodawcy</param>
        /// <param name="destinationAccountNumber">numer konta docelowego</param>
        /// <param name="ammount">kwota</param>
        /// <returns></returns>
        [HttpPost("transferMoney")]
        public async Task<IActionResult> TransferMoney([FromQuery]int principalAccountId, string destinationAccountNumber, decimal ammount)
        {
            var principalId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var accounts = await _accountRepo.DoTransferMoney(principalId, principalAccountId, destinationAccountNumber, ammount);

            if (accounts is null)
                return BadRequest();

            return Ok(accounts);
        }

        /// <summary>
        /// Akcja do usuwania (zmiany atrybutu na usuniete konta)
        /// </summary>
        /// <param name="accountId">id konta</param>
        /// <returns></returns>
        [HttpDelete("deleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromQuery]int accountId)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _accountRepo.DeleteAccount(currentUserId, accountId);

            if (result) return Ok("Konto zostało usunięte");
            else return BadRequest("Błąd przy usuwaniu");
            
        }
    }
}