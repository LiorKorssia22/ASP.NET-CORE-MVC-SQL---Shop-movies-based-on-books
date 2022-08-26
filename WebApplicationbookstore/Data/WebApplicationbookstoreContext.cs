using Microsoft.EntityFrameworkCore;

namespace WebApplicationbookstore.Data
{
    public class WebApplicationbookstoreContext : DbContext
    {
        public WebApplicationbookstoreContext(DbContextOptions<WebApplicationbookstoreContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplicationbookstore.Models.Book> Book { get; set; } = default!;
        public DbSet<WebApplicationbookstore.Models.Usersaccounts>? Usersaccounts { get; set; }
        public DbSet<WebApplicationbookstore.Models.Orders>? Orders { get; set; }
        public DbSet<WebApplicationbookstore.Models.Report>? Report { get; set; }
        public DbSet<WebApplicationbookstore.Models.Comment>? Comment { get; set; }
    }
}
