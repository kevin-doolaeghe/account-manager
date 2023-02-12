using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {

    [Route("api/record")]
    [ApiController]
    public class RecordController : ControllerBase {

        private readonly DatabaseContext _context;

        public RecordController(DatabaseContext context) { _context = context; }

        // GET: api/record
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordGetDto>>> GetAll() {
            return await _context.Records
                .Select(x => RecordGetDto.ToDto(x))
                .ToListAsync();
        }

        // GET: api/record/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordGetDto>> Get(long id) {
            var item = await _context.Records.FindAsync(id);
            if (item == null) return NotFound();

            return RecordGetDto.ToDto(item);
        }

        // PUT: api/record/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, RecordPostDto dto) {
            var item = await _context.Records.FindAsync(id);
            if (item == null) return NotFound();

            item.Description = dto.Description;
            item.Amount = dto.Amount;
            item.Status = dto.Status;
            item.Date = dto.Date;
            item.TypeId = dto.TypeId;
            item.AccountId = dto.AccountId;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) when (!Exists(id)) {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/record
        [HttpPost]
        public async Task<ActionResult<RecordGetDto>> Post(RecordPostDto dto) {
            var item = RecordPostDto.ToItem(dto);

            _context.Records.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = item.Id },
                RecordGetDto.ToDto(item)
            );
        }

        // DELETE: api/record/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) {
            var item = await _context.Records.FindAsync(id);
            if (item == null) return NotFound();

            _context.Records.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(long id) {
            return _context.Records.Any(e => e.Id == id);
        }
    }
}
