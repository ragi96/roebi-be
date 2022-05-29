using Roebi.LogManagment.Application.Repository;
using Roebi.PatientManagment.Application.Repository;
using Roebi.UserManagment.Application.Repository;

namespace Roebi.Common.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomRepository Room
        {
            get;
        }
        IUserRepository User
        {
            get;
        }

        ILogRepository Log
        {
            get;
        }

        IRoboterLogRepository RoboterLog
        {
            get;
        }


        IPatientRepository Patient
        {
            get;
        }


        IMedicineRepository Medicine
        {
            get;
        }
        IMedicationRepository Medication
        {
            get;
        }

        IJobRepository Job
        {
            get;
        }
        int Save();
    }
}
