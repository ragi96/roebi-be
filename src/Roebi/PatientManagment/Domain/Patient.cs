using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roebi.PatientManagment.Domain
{
    [Table("Patient")]
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Firstname { get; set; }
        public long EntryStamp { get; set; }
        public long ExitStamp { get; set; }

        [Column(TypeName = "text")]
        public string CaseHistory { get; set; }

        public Room Room { get; set; }

        public Patient() {

        }

        public Patient(string lastname, string firstname, long entrystamp, string casehistory) {
            LastName = lastname;
            Firstname = firstname;
            EntryStamp = entrystamp;
            CaseHistory = casehistory;
        }

        public Patient(int id, string lastname, string firstname, long entrystamp, long exitstamp, string casehistory)
        {
            Id = id;
            LastName = lastname;
            Firstname = firstname;
            EntryStamp = entrystamp;
            ExitStamp = exitstamp;
            CaseHistory = casehistory;
        }
    }
}
