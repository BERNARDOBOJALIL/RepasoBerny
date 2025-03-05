using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepasoBerny.Shared.DTO;
using RepasoBerny.Shared.Entities;
using RepasoBerny.Web.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepasoBerny.Web.Controllers
{
    public class AccountsController: Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;

        public AccountsController(IUserHelper userHelper, IConfiguration configuration)
        {
            this.userHelper = userHelper;
            this.configuration = configuration;
        }
        //Get
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(new LoginDTO());
        }
        // Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if(ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await userHelper.LoginAsync(loginDTO);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                   return RedirectToAction(nameof(Index), "Home");
                }
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrecta");
            }
            return View(loginDTO);
        }



        //[HttpPost("Login")]
        //public async Task<ActionResult> Login([FromBody]LoginDTO loginDTO)
        //{
        //    var result = await userHelper.LoginAsync(loginDTO);
        //    if (result.Succeeded)
        //    {
        //        var user = await userHelper.GetUserAsync(loginDTO.Email);
        //        return Ok(user);
        //    }
        //    return BadRequest("Useario o contraseña incorrecta");
        //}
        private object? BuildToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name,user.Email!),
        new Claim(ClaimTypes.Role,user.UserType.ToString()),
        new Claim("FirstName",user.FirstName),
        new Claim("LastName",user.LastName),
        new Claim("Photo",user.PhotoUrl ?? string.Empty),
        new Claim("CityId",user.CityId.ToString())
    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(10);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);
            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
