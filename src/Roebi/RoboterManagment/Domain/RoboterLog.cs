namespace Roebi.RoboterManagment.Domain

{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("RoboterLog")]
    public class RoboterLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long Timestamp { get; set; }

        public string Message { get; set; }

        public Type type { get; set; }

        public RoboterLog(string message) {
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            this.Message = message;
        }
    }
}
