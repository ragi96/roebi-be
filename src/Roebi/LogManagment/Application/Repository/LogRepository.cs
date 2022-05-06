using Roebi.Common.Repository;

namespace Roebi.LogManagment.Application.Repository
{
    using Roebi.Common.Context;
    using Roebi.LogManagment.Domain;

    class LogRepository : GenericRepository<Log>, ILogRepository
    {
        public LogRepository(RoebiContext context) : base(context) { }
    }
}
