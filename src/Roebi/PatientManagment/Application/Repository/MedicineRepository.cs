using Roebi.Common.Repository;
using Roebi.Common.Context;
using Roebi.PatientManagment.Domain;

namespace Roebi.PatientManagment.Application.Repository
{
    public interface IMedicineRepository : IGenericRepository<Medicine> { }

    class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(RoebiContext context) : base(context) { }
    }
}
