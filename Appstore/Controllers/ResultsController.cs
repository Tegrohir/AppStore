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
    public class ResultsController : ControllerBase
    {
        private readonly DbContextOptions<testmsdaContext> _dbOptions;

        public ResultsController(DbContextOptions<testmsdaContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        // GET: api/Results
        [HttpGet]
        public IEnumerable<Result> GetResult()
        {
            using (var context = new testmsdaContext(_dbOptions))
            {
                return context.Result.ToList();
            }
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResult([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                var result = await context.Result.FindAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
        }

        // PUT: api/Results/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult([FromRoute] int id, [FromBody] Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != result.ResultId)
            {
                return BadRequest();
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                context.Entry(result).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(id))
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

        // POST: api/Results
        [HttpPost]
        public async Task<IActionResult> PostResult([FromBody] Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                context.Result.Add(result);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetResult", new { id = result.ResultId }, result);
            }
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                var result = await context.Result.FindAsync(id);
                if (result == null)
                {
                    return NotFound();
                }

                context.Result.Remove(result);
                await context.SaveChangesAsync();

                return Ok(result);
            }
        }

        private bool ResultExists(int id)
        {
            using (var context = new testmsdaContext(_dbOptions))
            {
                return context.Result.Any(e => e.ResultId == id);
            }
        }
    }
}