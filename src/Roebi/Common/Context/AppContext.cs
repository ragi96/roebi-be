using Microsoft.EntityFrameworkCore;
using Roebi.PatientManagment.Domain;

namespace Roebi.Common.Context
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Room> Rooms { get; set; }
    }
}
