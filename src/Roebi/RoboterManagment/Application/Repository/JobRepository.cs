using Roebi.Common.Repository;

namespace Roebi.RoboterManagment.Application.Repository
{
    using Roebi.Common.Context;
    using Roebi.LogManagment.Application.Repository;
    using Roebi.RoboterManagment.Domain;

    class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(RoebiContext context) : base(context) { }
    }
}
