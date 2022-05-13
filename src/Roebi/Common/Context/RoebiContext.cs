using Microsoft.EntityFrameworkCore;
using Roebi.LogManagment.Domain;
using Roebi.PatientManagment.Domain;
using Roebi.RoboterManagment.Domain;
using Roebi.UserManagment.Domain;

namespace Roebi.Common.Context
{
    public class RoebiContext : DbContext
    {
        public RoebiContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Log> Logs { get; set; }
        public DbSet<RoboterLog> RoboterLogs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
