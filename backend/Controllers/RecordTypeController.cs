using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {

    [Route("api/record_type")]
    [ApiController]
    public class RecordTypeController : ControllerBase {

        private readonly DatabaseContext _context;

        public RecordTypeController(DatabaseContext context) { _context = context; }

        // GET: api/record_type
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordTypeGetDto>>> GetAll() {
            return await _context.RecordTypes
                .Select(x => RecordTypeGetDto.ToDto(x))
                .ToListAsync();
        }

        // GET: api/record_type/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordTypeGetDto>> Get(long id) {
            var item = await _context.RecordTypes.FindAsync(id);
            if (item == null) return NotFound();

            return RecordTypeGetDto.ToDto(item);
        }

        // PUT: api/record_type/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, RecordTypePostDto dto) {
            var item = await _context.RecordTypes.FindAsync(id);
            if (item == null) return NotFound();

            item.Name = dto.Name;
            item.Icon = dto.Icon;
            item.Color = dto.Color;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) when (!Exists(id)) {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/record_type
        [HttpPost]
        public async Task<ActionResult<RecordTypeGetDto>> Post(RecordTypePostDto dto) {
            var item = RecordTypePostDto.ToItem(dto);

            _context.RecordTypes.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = item.RecordTypeId },
                RecordTypeGetDto.ToDto(item)
            );
        }

        // DELETE: api/record_type/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) {
            var item = await _context.RecordTypes.FindAsync(id);
            if (item == null) return NotFound();

            _context.RecordTypes.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(long id) {
            return _context.RecordTypes.Any(e => e.RecordTypeId == id);
        }
    }
}
