using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace backend.Controllers {

    [Route("api/balance")]
    [ApiController]
    public class BalanceController : ControllerBase {

        private readonly DatabaseContext _context;

        public BalanceController(DatabaseContext context) { _context = context; }

        // GET: api/balance/5
        [HttpGet("{accountId}")]
        public async Task<ActionResult<double>> GetBalance(long accountId) {
            return await _context.Records
                .Where(x => x.AccountId == accountId)
                .SumAsync(x => x.Amount);
        }

        // GET: api/balance/5/2
        [HttpGet("{accountId}/{recordTypeId}")]
        public async Task<ActionResult<double>> GetBalance(long accountId, long recordTypeId) {
            return await _context.Records
                .Where(x => x.AccountId == accountId)
                .Where(x => x.RecordTypeId == recordTypeId)
                .SumAsync(x => x.Amount);
        }
    }
}
