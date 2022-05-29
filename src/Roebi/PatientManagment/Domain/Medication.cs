using Roebi.RoboterManagment.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roebi.PatientManagment.Domain
{
    public class Medication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Medicine Medicine { get; set; }

        [Required]
        public Patient Patient { get; set; }

        [Required]
        public long TakingStamp { get; set; }

        public Job? Job { get; set; }
    }
}
