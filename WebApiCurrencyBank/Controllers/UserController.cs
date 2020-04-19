using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurrencyBank.Database.Data;
using CurrencyBank.Database.Models;
using Microsoft.AspNetCore.Authorization;
using CurrencyBank.API.Interfaces;
using CurrencyBank.API.Dtos;
using AutoMapper;
using System.Security.Claims;

namespace CurrencyBank.API.Controllers
{
    /// <summary>
    /// Kontoler do zarzadzania uzytkownikami
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _repo = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Akcja do pobierania wszystkich uzytkownikow wraz z ich kontami
        /// </summary>
        /// <returns>lista uzytkownikow wraz z kontami</returns>
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            if (!users.Any())
                return NotFound();

            return Ok(users);
        }

        /// <summary>
        /// Akcja do pobierania konkretnego uzytkownika wraz z kontem / kontami
        /// </summary>
        /// <param name="id">id usera</param>
        /// <returns>uzytkownik wraz z kontem/kontami</returns>
        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserToReturnDto>> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            if (user == null)
                return NotFound();

            var userToReturn = _mapper.Map<UserToReturnDto>(user);

            return Ok(userToReturn);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserForUpdateDto userForUpdateDto)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userForUpdateDto.Id)
                return BadRequest();

            var updatedUser = await _repo.UpdateUser(userForUpdateDto);
            var userToReturn = _mapper.Map<UserToReturnDto>(updatedUser);
            if (updatedUser == null)
                return BadRequest();

            return Ok(userToReturn);
        }

        // POST: api/User
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _repo.Add(user);
            if (await _repo.SaveAll())
                return CreatedAtAction("GetUser", new { id = user.Id }, user);

            return BadRequest();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var userToDelete = await _repo.GetUser(id);

            _repo.Delete(userToDelete);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest();
            
        }
    }
}
