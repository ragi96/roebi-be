using Roebi.Common.Repository;
using Roebi.Common.Context;
using Roebi.PatientManagment.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Roebi.PatientManagment.Application.Repository
{
    public interface IMedicationRepository : IGenericRepository<Medication> { }

    class MedicationRepository : GenericRepository<Medication>, IMedicationRepository
    {
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
    }
}
