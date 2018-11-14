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
    public class DevicesController : ControllerBase
    {
        private readonly DbContextOptions<testmsdaContext> _dbOptions;

        public DevicesController(DbContextOptions<testmsdaContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        // GET: api/Devices
        [HttpGet]
        public IEnumerable<Device> GetDevice()
        {
            using (var context = new testmsdaContext(_dbOptions))
            {
                return context.Device.ToList();
            }                
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                var device = await context.Device.FindAsync(id);

                if (device == null)
                {
                    return NotFound();
                }

                return Ok(device);
            }
        }

        // PUT: api/Devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice([FromRoute] int id, [FromBody] Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.DeviceId)
            {
                return BadRequest();
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                context.Entry(device).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(id))
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

        // POST: api/Devices
        [HttpPost]
        public async Task<IActionResult> PostDevice([FromBody] Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                context.Device.Add(device);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
            }
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new testmsdaContext(_dbOptions))
            {
                var device = await context.Device.FindAsync(id);
                if (device == null)
                {
                    return NotFound();
                }

                context.Device.Remove(device);
                await context.SaveChangesAsync();

                return Ok(device);
            }
        }

        private bool DeviceExists(int id)
        {
            using (var context = new testmsdaContext(_dbOptions))
            {
                return context.Device.Any(e => e.DeviceId == id);
            }
        }
    }
}