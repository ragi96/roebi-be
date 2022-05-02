using Microsoft.EntityFrameworkCore;
using Roebi.PatientManagment.Domain;
using Roebi.UserManagment.Domain;

namespace Roebi.Common.Context
{
    public class RoebiContext : DbContext
    {
        public RoebiContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
