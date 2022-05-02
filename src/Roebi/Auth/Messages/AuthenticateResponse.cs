using Roebi.UserManagment.Domain;

namespace Roebi.Auth.Messages
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Role = user.Role;
            Token = token;
        }
    }
}
