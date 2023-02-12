using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;

namespace backend.Services {

    public class DatabaseContext : DbContext {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Account> Accounts { get; set; } = null!;

        public DbSet<Record> Records { get; set; } = null!;

        public DbSet<RecordType> RecordTypes { get; set; } = null!;
    }
}
