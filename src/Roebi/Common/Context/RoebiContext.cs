using Microsoft.EntityFrameworkCore;
using Roebi.PatientManagment.Domain;

namespace Roebi.Common.Context
{
    public class RoebiContext : DbContext
    {
        public RoebiContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Room> Rooms { get; set; }
    }
}
