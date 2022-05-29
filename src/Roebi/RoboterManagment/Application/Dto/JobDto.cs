using Roebi.PatientManagment.Domain;
using Roebi.RoboterManagment.Domain;

namespace Roebi.RoboterManagment.Application.Dto
{
    public class CreatedJob
    {
        public long Id { get; set; }

        public JobState State { get; set; }

        public IEnumerable<Medication>? Medication { get; set; }
    }
}
