using Roebi.Common.Repository;
using Roebi.Common.Context;
using Roebi.PatientManagment.Domain;

namespace Roebi.PatientManagment.Application.Repository
{
    public interface IRoomRepository : IGenericRepository<Room> { }
    class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(RoebiContext context) : base(context) { }
    }
}
