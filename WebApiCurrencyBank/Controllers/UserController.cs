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

        public UserController(IUserRepository userRepository)
        {
            _repo = userRepository;
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
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            var updatedUser = await _repo.UpdateUser(user);

            if (updatedUser == null)
                return BadRequest();

            return Ok(user);
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
