
using WebApplication23.Entities;

namespace WebApplication23.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
        public string Avatar { get; set; }

        public int RoleId { get; set; }
        public int IsActive { get; set; }

        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Username = user.Username;
            Email = user.Email;
            Avatar = user.Avatar;
            IsActive = user.IsActive;
            RoleId = user.RoleId;
            Token = token;
        }
    }
}