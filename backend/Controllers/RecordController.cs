using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {

    [Route("api/records")]
    [ApiController]
    public class RecordController : ControllerBase {

        private readonly DatabaseContext _context;

        public RecordController(DatabaseContext context) { _context = context; }

        // GET: api/records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordGetDto>>> GetAll() {
            return await _context.Records
                .Select(x => RecordGetDto.ToDto(x))
                .ToListAsync();
        }

        // GET: api/records/5
        [HttpGet("{recordId}")]
        public async Task<ActionResult<RecordGetDto>> Get(long recordId) {
            var item = await _context.Records.FindAsync(recordId);
            if (item == null) return NotFound();

            return RecordGetDto.ToDto(item);
        }

        // PUT: api/records/5
        [HttpPut("{recordId}")]
        public async Task<IActionResult> Put(long recordId, RecordPostDto dto) {
            if (!RecordTypeExists(dto.RecordTypeId) || !AccountExists(dto.AccountId)) return NotFound();

            var item = await _context.Records.FindAsync(recordId);
            if (item == null) return NotFound();

            item.Description = dto.Description;
            item.Amount = dto.Amount;
            item.Status = dto.Status;
            item.Date = dto.Date;
            item.RecordTypeId = dto.RecordTypeId;
            item.AccountId = dto.AccountId;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) when (!RecordExists(recordId)) {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/records
        [HttpPost]
        public async Task<ActionResult<RecordGetDto>> Post(RecordPostDto dto) {
            if (!RecordTypeExists(dto.RecordTypeId) || !AccountExists(dto.AccountId)) return NotFound();

            var item = RecordPostDto.ToItem(dto);

            _context.Records.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { recordId = item.RecordId },
                RecordGetDto.ToDto(item)
            );
        }

        // DELETE: api/records/5
        [HttpDelete("{recordId}")]
        public async Task<IActionResult> Delete(long recordId) {
            var item = await _context.Records.FindAsync(recordId);
            if (item == null) return NotFound();

            _context.Records.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecordExists(long recordId) {
            return _context.Records.Any(e => e.RecordId == recordId);
        }

        private bool RecordTypeExists(long recordTypeId) {
            return _context.RecordTypes.Any(e => e.RecordTypeId == recordTypeId);
        }

        private bool AccountExists(long accountId) {
            return _context.Accounts.Any(e => e.AccountId == accountId);
        }
    }
}
