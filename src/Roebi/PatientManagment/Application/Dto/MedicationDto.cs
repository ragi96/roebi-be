namespace Roebi.PatientManagment.Application.Dto
{
    public class MedicationDto
    {
        public int Id { get; set; }
        public int Medicine { get; set; }
        public int Patient { get; set; }
        public long TakingStamp { get; set; }
    }
}
