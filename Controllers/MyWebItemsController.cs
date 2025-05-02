using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Visual.myWebAPI.Models;

namespace Visual.myWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyWebItemsController : ControllerBase
    {
        private readonly MyWebContext _context;

        public MyWebItemsController(MyWebContext context)
        {
            _context = context;
        }

        [HttpGet] // GET: api/MyWebItems
        public async Task<ActionResult<IEnumerable<MyWebItem_DTO>>> GetMyWebItems()
        {
            return await _context.MyWebItems
            .Select(x => MyWebItem_DTO(x))
            .ToListAsync();
        }

        [HttpGet("{id}")] // GET: api/MyWebItems/5
        public async Task<ActionResult<MyWebItem_DTO>> GetMyWebItem(long id)
        {
            var myWebItem = await _context.MyWebItems.FindAsync(id);

            if (myWebItem == null)
            {
                return NotFound();
            }

            return MyWebItem_DTO(myWebItem);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")] // PUT: api/MyWebItems/5
        public async Task<IActionResult> PutMyWebItem(long id, MyWebItem_DTO myWebItem_DTO)
        {
            if (id != myWebItem_DTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(myWebItem_DTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyWebItemExists(id))
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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost] // POST: api/MyWebItems
        public async Task<ActionResult<MyWebItem_DTO>> PostMyWebItem(MyWebItem_DTO myWebItem_DTO)
        {
            var myWebItem = new MyWebItem
            {
                IsComplete = myWebItem_DTO.IsComplete,
                Name = myWebItem_DTO.Name
            };

            _context.MyWebItems.Add(myWebItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMyWebItem), new { id = myWebItem.Id }, myWebItem);
        }

        [HttpDelete("{id}")] // DELETE: api/MyWebItems/5
        public async Task<IActionResult> DeleteMyWebItem(long id)
        {
            var myWebItem = await _context.MyWebItems.FindAsync(id);
            if (myWebItem == null)
            {
                return NotFound();
            }

            _context.MyWebItems.Remove(myWebItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MyWebItemExists(long id)
        {
            return _context.MyWebItems.Any(e => e.Id == id);
        }

        private static MyWebItem_DTO MyWebItem_DTO(MyWebItem MyWebItem) =>
        new()
        {
            Id = MyWebItem.Id,
            Name = MyWebItem.Name,
            IsComplete = MyWebItem.IsComplete
        };
    }
}
