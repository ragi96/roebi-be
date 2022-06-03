namespace Roebi.PatientManagment.Application.Dto
{
    public class AddPatientDto
    {
        public string LastName { get; set; }
        public string Firstname { get; set; }
        public long EntryStamp { get; set; }
        public string CaseHistory { get; set; }
        public int Room { get; set; } 
    }

    public class UpdatePatientDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Firstname { get; set; }
        public long EntryStamp { get; set; }
        public long ExitStamp { get; set; }
        public string CaseHistory { get; set; }
        public int Room { get; set; }
    }
}
