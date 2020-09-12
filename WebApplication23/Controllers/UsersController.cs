using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using WebApplication23.Entities;
using WebApplication23.Helpers;
using WebApplication23.Models;
using WebApplication23.Services;

namespace WebApplication23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _singInManager;
        private readonly AppSettings _appSettings;


        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        [HttpPost("getUsers")]
        public IActionResult getUsers([FromBody] User user)
        {
            var response = _userService.GetByName(user.Username);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        
        [HttpPost("check")]
        public IActionResult check([FromBody] GetID user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("NGUYENTHUONGHOAIIN0NOml3z9FMfmpgXwovR9fp6ryDIoGRM8EPHAB6iHsc0fb");
                tokenHandler.ValidateToken(user.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                // attach user to context on successful jwt validation
                var response = _userService.GetById(userId);
                return Ok(response);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
                return Ok("lỗi");
            }

            return Ok("");
        }


        [HttpPost("signup")]
        public IActionResult signup([FromBody] User user)
        {
            if (user.RoleId == 0)
                user.RoleId = 2;
            if (user.IsActive == 0)
                user.IsActive = 1;
            try
            {
                if (!_userService.checkUser(user.Username))
                {
                    var response = _userService.setUserToken(user);
                    return Ok(response);
                }
                else return BadRequest(new { message = "Username has used" });
            }
            catch (Exception e)
            {
                //return BadRequest(new { message = "BadRequest" });
                return Ok("lỗi"+e.Message);
            }
            return Ok(user);
        }


        
        [HttpPost("checkUserName")]
        public IActionResult checkUserName([FromBody] UsernameObj user)
        {

            return Ok(_userService.checkUser(user.Username));

        }

       
    }
}
