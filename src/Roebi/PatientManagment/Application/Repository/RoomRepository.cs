using Roebi.Common.Repository;

namespace Roebi.PatientManagment.Application.Repository
{
    using Roebi.Common.Context;
    using Roebi.PatientManagment.Domain;

    class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(RoebiContext context) : base(context) { }
    }
}
