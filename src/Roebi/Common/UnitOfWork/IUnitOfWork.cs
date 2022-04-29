using Roebi.PatientManagment.Application.Repository;

namespace Roebi.Common.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomRepository Room
        {
            get;
        }
        int Save();
    }
}
