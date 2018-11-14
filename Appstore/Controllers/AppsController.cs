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
    public class AppsController : ControllerBase
    {
        private readonly DbContextOptions<testmsdaContext> _dbOptions;

        public AppsController(DbContextOptions<testmsdaContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        // GET: api/Apps
        [HttpGet]
        public IEnumerable<App> GetApp()
        {
            using (var context = new testmsdaContext(_dbOptions))
            {
                return context.App.ToList();
            }
        }

        // GET: api/Apps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApp([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                var app = await context.App.FindAsync(id);

                if (app == null)
                {
                    return NotFound();
                }

                return Ok(app);
            }
        }

        // PUT: api/Apps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApp([FromRoute] int id, [FromBody] App app)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != app.AppId)
            {
                return BadRequest();
            }

            using (var context = new testmsdaContext(_dbOptions))
            {

                context.Entry(app).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppExists(id, context))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            }

        // POST: api/Apps
        [HttpPost]
        public async Task<IActionResult> PostApp([FromBody] App app)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {

                context.App.Add(app);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetApp", new { id = app.AppId }, app);
            }
        }

        // DELETE: api/Apps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApp([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                var app = await context.App.FindAsync(id);
                if (app == null)
                {
                    return NotFound();
                }

                context.App.Remove(app);
                await context.SaveChangesAsync();

                return Ok(app);
            }
        }

        private bool AppExists(int id, testmsdaContext context)
        {
            return context.App.Any(e => e.AppId == id);
        }
    }
}