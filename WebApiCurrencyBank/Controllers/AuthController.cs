using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CurrencyBank.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CurrencyBank.API.Dtos;
using CurrencyBank.API.Interfaces;
using CurrencyBank.API.Helpers.Exceptions;

namespace CurrencyBank.API.Controllers
{
    /// <summary>
    /// Kontroler do zarzadzania autoryzacja
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        /// <summary>
        /// Akcja sluzaca do rejestracji uzytkownika
        /// </summary>
        /// <param name="userRegisterDto">dto userRegister</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.UserName = userRegisterDto.UserName.ToLower();

            if (await _repo.UserExists(userRegisterDto.UserName))
                return BadRequest("The same username already exists");

            var userToCreate = _mapper.Map<User>(userRegisterDto);
            try
            {
                var createdUser = _repo.Register(userToCreate, userRegisterDto.Password);
                if (createdUser.Result == null)
                {
                    return BadRequest("Error occured");
                }
                return Created("", createdUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message.ToString());
            }
        }

        /// <summary>
        /// Akcja sluzaca do logowania zwracajaca token dostepu
        /// </summary>
        /// <param name="userLoginDto">dto userLoginDto</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userFromRepo = await _repo.Login(userLoginDto.UserName.ToLower(), userLoginDto.Password);

            if (userFromRepo == null) return Unauthorized();

            var userToReturn = _mapper.Map<UserToReturnDto>(userFromRepo);

            //create token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            userToReturn.Token = tokenHandler.WriteToken(token);

            return Ok(new
            {
                userToReturn
            });
        }
    }
}