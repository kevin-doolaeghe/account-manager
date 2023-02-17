using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.DTOs;
using backend.Models;
using backend.Services;

namespace backend.Controllers {

    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly DatabaseContext _context;

        public UserController(DatabaseContext context) { _context = context; }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetDto>>> GetAll() {
            return await _context.Users
                .Select(x => UserGetDto.ToDto(x))
                .ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserGetDto>> Get(long userId) {
            var item = await _context.Users.FindAsync(userId);
            if (item == null) return NotFound();

            return UserGetDto.ToDto(item);
        }

        // PUT: api/users/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(long userId, UserPostDto dto) {
            var item = await _context.Users.FindAsync(userId);
            if (item == null) return NotFound();

            item.Name = dto.Name;
            item.Email = dto.Email;
            item.Password = dto.Password;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) when (!UserExists(userId)) {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserGetDto>> Post(UserPostDto dto) {
            var item = UserPostDto.ToItem(dto);

            _context.Users.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { userId = item.UserId },
                UserGetDto.ToDto(item)
            );
        }

        // DELETE: api/users/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(long userId) {
            var item = await _context.Users.FindAsync(userId);
            if (item == null) return NotFound();

            _context.Users.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long userId) {
            return _context.Users.Any(e => e.UserId == userId);
        }
    }
}
