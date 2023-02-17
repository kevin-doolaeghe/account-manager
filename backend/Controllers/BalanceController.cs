using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {

    [Route("api/balance")]
    [ApiController]
    public class BalanceController : ControllerBase {

        private readonly DatabaseContext _context;

        public BalanceController(DatabaseContext context) { _context = context; }

        // GET: api/balance/5
        [HttpGet]
        public async Task<ActionResult<double>> GetBalance(long accoundId) {
            return await _context.Records
                .Where(x => x.AccountId == accoundId)
                .SumAsync(x => x.Amount);
        }

        // GET: api/balance/5/2
        [HttpGet]
        public async Task<ActionResult<double>> GetBalance(long accoundId, long recordId) {
            return await _context.Records
                .Where(x => x.AccountId == accoundId)
                .Where(x => x.RecordId == recordId)
                .SumAsync(x => x.Amount);
        }
    }
}
