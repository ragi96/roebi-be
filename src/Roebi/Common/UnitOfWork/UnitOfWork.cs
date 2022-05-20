using Roebi.Common.Context;
using Roebi.LogManagment.Application.Repository;
using Roebi.PatientManagment.Application.Repository;
using Roebi.RoboterManagment.Application.Repository;
using Roebi.UserManagment.Application.Repository;

namespace Roebi.Common.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private RoebiContext _context;
        public UnitOfWork(RoebiContext context)
        {
            _context = context;
            Room = new RoomRepository(_context);
            User = new UserRepository(_context);
            Log = new LogRepository(_context);
            RoboterLog = new RoboterLogRepository(_context);
            Patient = new PatientRepository(_context);
            Medicine = new MedicineRepository(_context);
        }

        public IMedicineRepository Medicine
        {
            get;
            private set;
        }

        public IPatientRepository Patient
        {
            get;
            private set;
        }
        public IRoomRepository Room
        {
            get;
            private set;
        }

        public IUserRepository User
        {
            get;
            private set;
        }

        public ILogRepository Log
        {
            get;
            private set;
        }

        public IRoboterLogRepository RoboterLog
        {
            get;
            private set;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
