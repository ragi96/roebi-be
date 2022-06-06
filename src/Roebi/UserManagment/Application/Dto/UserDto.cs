using Roebi.UserManagment.Domain;
using System.ComponentModel.DataAnnotations;

namespace Roebi.UserManagment.Application.Dto
{
    public class UpdateCurrentUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }

    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
    }

    public class AddUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string PasswordHash { get; set; }
    }

    public class PasswordCurrentUpdate
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }

    public class PasswordUpdate
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

