using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApplication23.Entities;
using WebApplication23.Helpers;
using WebApplication23.Models;

namespace WebApplication23.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetByName(String name);
        User GetById(int id);
        AuthenticateResponse setUserToken(User user);
        bool checkUser(String name);
    }

    public class UserService : IUserService
    {
        

        private List<User> _users = new List<User>();
        private readonly AppSettings _appSettings;
        private DataContext db;

        public UserService(IOptions<AppSettings> appSettings)
        {
            db = new DataContext();
            
            _users = db.Users.ToList();
            
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            
            return _users;
        }

        public User GetByName(String name)
        {
            return _users.SingleOrDefault(x => x.Username == name);
        }

        // helper methods

        public string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool checkUser(String name)
        {
            
            return _users.FirstOrDefault(x => x.Username == name)!=null;
        }

        public AuthenticateResponse setUserToken(User user)
        {
            
            db.Users.Add(user);
            db.SaveChanges();
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
    }
}