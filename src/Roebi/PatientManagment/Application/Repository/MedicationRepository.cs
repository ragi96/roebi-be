using Roebi.Common.Repository;
using Roebi.Common.Context;
using Roebi.PatientManagment.Domain;

namespace Roebi.PatientManagment.Application.Repository
{
    public interface IMedicationRepository : IGenericRepository<Medication> { }

    class MedicationRepository : GenericRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(RoebiContext context) : base(context) { }
    }
}
