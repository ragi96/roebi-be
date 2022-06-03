using Roebi.UserManagment.Domain;

namespace Roebi.Auth.Messages
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            User = user;
            Token = token;
        }
    }
}
