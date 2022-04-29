using Roebi.Common.Context;
using Roebi.PatientManagment.Application.Repository;

namespace Roebi.Common.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private RoebiContext context;
        public UnitOfWork(RoebiContext context)
        {
            this.context = context;
            Address = new RoomRepository(this.context);
        }
        public IRoomRepository Address
        {
            get;
            private set;
        }

        IRoomRepository IUnitOfWork.Address => throw new NotImplementedException();

        public void Dispose()
        {
            context.Dispose();
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
