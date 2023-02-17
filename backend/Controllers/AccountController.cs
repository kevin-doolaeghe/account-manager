using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.DTOs;
using backend.Models;
using backend.Services;

namespace backend.Controllers {

    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase {

        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context) { _context = context; }

        // GET: api/accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountGetDto>>> GetAll() {
            return await _context.Accounts
                .Select(x => AccountGetDto.ToDto(x))
                .ToListAsync();
        }

        // GET: api/accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountGetDto>> Get(long id) {
            var item = await _context.Accounts.FindAsync(id);
            if (item == null) return NotFound();

            return AccountGetDto.ToDto(item);
        }

        // PUT: api/accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, AccountPostDto dto) {
            if (!UserExists(dto.UserId)) return NotFound();

            var item = await _context.Accounts.FindAsync(id);
            if (item == null) return NotFound();

            item.Name = dto.Name;
            item.UserId = dto.UserId;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) when (!AccountExists(id)) {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<ActionResult<AccountGetDto>> Post(AccountPostDto dto) {
            if (!UserExists(dto.UserId)) return NotFound();

            var item = AccountPostDto.ToItem(dto);

            _context.Accounts.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = item.AccountId },
                AccountGetDto.ToDto(item)
            );
        }

        // DELETE: api/accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) {
            var item = await _context.Accounts.FindAsync(id);
            if (item == null) return NotFound();

            _context.Accounts.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(long id) {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

        private bool UserExists(long id) {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
