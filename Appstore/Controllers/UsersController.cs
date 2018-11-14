using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppstoreAPI.Models;

namespace AppstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbContextOptions<testmsdaContext> _dbOptions;

        public UsersController(DbContextOptions<testmsdaContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            using (var context = new testmsdaContext(_dbOptions))
            {
                return context.User.ToList();
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                var user = await context.User.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                context.Entry(user).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                context.User.Add(user);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                var user = await context.User.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                context.User.Remove(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
        }

        private bool UserExists(int id)
        {
            using (var context = new testmsdaContext(_dbOptions))
            {
                return context.User.Any(e => e.UserId == id);
            }
        }
    }
}