using Roebi.Common.Context;
using Roebi.PatientManagment.Application.Repository;

namespace Roebi.Common.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private RoebiContext context;
        public UnitOfWork(RoebiContext context)
        {
            this.context = context;
            Room = new RoomRepository(this.context);
        }
        public IRoomRepository Room
        {
            get;
            private set;
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
