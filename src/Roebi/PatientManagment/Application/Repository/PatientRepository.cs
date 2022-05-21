using Roebi.Common.Repository;
using Roebi.Common.Context;
using Roebi.PatientManagment.Domain;
using Microsoft.EntityFrameworkCore;

namespace Roebi.PatientManagment.Application.Repository
{
    public interface IPatientRepository : IGenericRepository<Patient> { }

    class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        protected readonly new RoebiContext _context;
        public PatientRepository(RoebiContext context) : base(context) {
            _context = context;
        }

        public new IEnumerable<Patient> GetAll()
        {
            return _context.Set<Patient>().Include(p => p.Room).ToList();
        }

        public new Patient GetById(int id)
        {
            return _context.Set<Patient>().Include(p => p.Room).SingleOrDefault(x => x.Id == id);
        }
    }
}
