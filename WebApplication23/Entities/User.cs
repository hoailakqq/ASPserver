using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication23.Entities
{
    [Table("user")]
    public class User
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User()
        {
        }

        public User(string username, string password, string name, string email, string avatar, int roleId, int isActive) : this(username, password)
        {
            Name = name;
            Email = email;
            Avatar = avatar;
            RoleId = roleId;
            IsActive = isActive;
        }

        [Key]
        public int Id { get; set; }
        //[JsonIgnore] not show this field
        public string Name { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        public string Email { get; set; }
        public string Avatar { get; set; }

        public int RoleId { get; set; }
        public int IsActive { get; set; }
    }
}