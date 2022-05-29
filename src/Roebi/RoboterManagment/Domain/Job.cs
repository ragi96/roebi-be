using System.ComponentModel.DataAnnotations;

namespace Roebi.RoboterManagment.Domain
{
    public class Job
    {
        [Key]
        public long Id { get; set; }
        public JobState State { get; set; }

        public Job() {
            Id = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            State = JobState.Created;
        }
    }
}
