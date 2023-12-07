using gnisbdback.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace gnisbdback.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CorporateCustomers> Corporate_Customer_Tbl { get; set; }

        public DbSet<IndividualCustomers> Individual_Customer_Tbl { get; set; }

        public DbSet<ProductServices> Products_Service_tbl { get; set; }

        public DbSet<MeetingMInutesMaster> Meeting_Minutes_Master_Tbl { get; set; }

        public DbSet<MeetingMinutesDetails> Meeting_Minutes_Details_Tbl { get; set; }
    }
}
