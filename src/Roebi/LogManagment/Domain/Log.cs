namespace Roebi.LogManagment.Domain

{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Log")]
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long Timestamp { get; set; }

        public string Message { get; set; }

        public Log(string message) {
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            this.Message = message;
        }
    }
}
