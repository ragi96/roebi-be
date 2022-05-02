namespace Roebi.UserManagment.Domain
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }
        public Role Role { get; set; }

        public string PasswordHash { get; set; }
    }
}
