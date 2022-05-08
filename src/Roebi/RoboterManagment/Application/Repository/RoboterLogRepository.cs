using Roebi.Common.Repository;

namespace Roebi.RoboterManagment.Application.Repository
{
    using Roebi.Common.Context;
    using Roebi.LogManagment.Application.Repository;
    using Roebi.LogManagment.Domain;
    using Roebi.RoboterManagment.Domain;

    class RoboterLogRepository : GenericRepository<RoboterLog>, IRoboterLogRepository
    {
        public RoboterLogRepository(RoebiContext context) : base(context) { }
    }
}
