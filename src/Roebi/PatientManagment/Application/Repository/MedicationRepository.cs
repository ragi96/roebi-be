using Roebi.Common.Repository;
using Roebi.Common.Context;
using Roebi.PatientManagment.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Roebi.PatientManagment.Application.Repository
{
    public interface IMedicationRepository : IGenericRepository<Medication> {
        public IEnumerable<Medication> FindForJob(long actualTimestamp);
    }

    class MedicationRepository : GenericRepository<Medication>, IMedicationRepository
    {

        private static int JOB_DURATION = 10 * 60 * 1000;
        private static int MEDICATION_PER_JOB = 4;
        public MedicationRepository(RoebiContext context) : base(context) { }

        public new IEnumerable<Medication> GetAll()
        {
            return _context.Set<Medication>().Include(medication => medication.Patient)
                .ThenInclude(patient => patient.Room)
                .Include(medication => medication.Medicine)
                .ToList();
        }

        public new Medication GetById(int id)
        {
            return _context.Set<Medication>().Include(medication => medication.Patient)
                .ThenInclude(patient => patient.Room)
                .Include(medication => medication.Medicine)
                .SingleOrDefault(x => x.Id == id);
        }

        public new IEnumerable<Medication> Find(Expression<Func<Medication, bool>> expression)
        {
            return _context.Set<Medication>().Where(expression).OrderBy(medication => medication.TakingStamp).Include(medication => medication.Patient)
                .ThenInclude(patient => patient.Room)
                .Include(medication => medication.Medicine);
        }

        public IEnumerable<Medication> FindForJob(long actualTimestamp) {
            var endJobStamp = actualTimestamp + JOB_DURATION;
            return _context.Set<Medication>().Where(medi => medi.TakingStamp >= actualTimestamp && medi.TakingStamp <= endJobStamp && medi.Job == null).OrderBy(medication => medication.TakingStamp).Take(MEDICATION_PER_JOB).Include(medication => medication.Patient)
                .ThenInclude(patient => patient.Room)
                .Include(medication => medication.Medicine);
        }
    }
}
