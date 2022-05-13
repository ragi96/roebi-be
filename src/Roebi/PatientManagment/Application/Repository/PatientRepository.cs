using Roebi.Common.Repository;
using Roebi.Common.Context;
using Roebi.PatientManagment.Domain;

namespace Roebi.PatientManagment.Application.Repository
{
    public interface IPatientRepository : IGenericRepository<Patient> { }

    class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(RoebiContext context) : base(context) { }
    }
}
