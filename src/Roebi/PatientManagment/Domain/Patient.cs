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
    }
}
