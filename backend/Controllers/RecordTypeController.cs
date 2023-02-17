using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {

    [Route("api/record_types")]
    [ApiController]
    public class RecordTypeController : ControllerBase {

        private readonly DatabaseContext _context;

        public RecordTypeController(DatabaseContext context) { _context = context; }

        // GET: api/record_types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordTypeGetDto>>> GetAll() {
            return await _context.RecordTypes
                .Select(x => RecordTypeGetDto.ToDto(x))
                .ToListAsync();
        }

        // GET: api/record_types/5
        [HttpGet("{recordTypeId}")]
        public async Task<ActionResult<RecordTypeGetDto>> Get(long recordTypeId) {
            var item = await _context.RecordTypes.FindAsync(recordTypeId);
            if (item == null) return NotFound();

            return RecordTypeGetDto.ToDto(item);
        }

        // PUT: api/record_types/5
        [HttpPut("{recordTypeId}")]
        public async Task<IActionResult> Put(long recordTypeId, RecordTypePostDto dto) {
            var item = await _context.RecordTypes.FindAsync(recordTypeId);
            if (item == null) return NotFound();

            item.Name = dto.Name;
            item.Icon = dto.Icon;
            item.Color = dto.Color;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) when (!RecordTypeExists(recordTypeId)) {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/record_types
        [HttpPost]
        public async Task<ActionResult<RecordTypeGetDto>> Post(RecordTypePostDto dto) {
            var item = RecordTypePostDto.ToItem(dto);

            _context.RecordTypes.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { recordTypeId = item.RecordTypeId },
                RecordTypeGetDto.ToDto(item)
            );
        }

        // DELETE: api/record_types/5
        [HttpDelete("{recordTypeId}")]
        public async Task<IActionResult> Delete(long recordTypeId) {
            var item = await _context.RecordTypes.FindAsync(recordTypeId);
            if (item == null) return NotFound();

            _context.RecordTypes.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecordTypeExists(long recordTypeId) {
            return _context.RecordTypes.Any(e => e.RecordTypeId == recordTypeId);
        }
    }
}
