using Roebi.PatientManagment.Application.Repository;

namespace Roebi.Common.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomRepository Address
        {
            get;
        }
        int Save();
    }
}
