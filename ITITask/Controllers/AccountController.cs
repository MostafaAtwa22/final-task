    using ITITask.DTO;
    using ITITask.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    namespace ITITask.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class AccountController : ControllerBase
        {
            private readonly UserManager<ApplicationUser> userManager;
            private readonly IConfiguration configuration;

            public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
            {
                this.userManager = userManager;
                this.configuration = configuration;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser();
                    user.UserName = registerUserDTO.UserName;
                    IdentityResult result = await userManager.CreateAsync(user, registerUserDTO.Password);

                    if (result.Succeeded)
                    {
                        return Ok("Account Created Successfuly !!");
                    }
                    foreach (var item in result.Errors)
                    {
                        return BadRequest(item);
                    }
                }
                return BadRequest(ModelState);
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByNameAsync(loginUserDTO.UserName);
                    if (user is not null)
                    {
                        bool foundPassword = await userManager.CheckPasswordAsync(user, loginUserDTO.Password);
                        if (foundPassword)
                        {
                            var claims = new List<Claim>();
                            claims.Add(new Claim(ClaimTypes.Name, loginUserDTO.UserName));
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                            var roles = await userManager.GetRolesAsync(user);
                            foreach (var role in roles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role));
                            }

                            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
                            SigningCredentials signingCredentials = new SigningCredentials(
                                    key,
                                    SecurityAlgorithms.HmacSha256
                                );

                            // create Token
                            JwtSecurityToken token = new JwtSecurityToken(
                                    issuer: configuration["JWT:ValidIssuer"],
                                    audience: configuration["JWT:ValidAudiance"],
                                    claims: claims,
                                    expires: DateTime.Now.AddHours(1),
                                    signingCredentials: signingCredentials
                                );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                        }
                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }
        }
    }
