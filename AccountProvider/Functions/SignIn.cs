using AccountProvider.RequestModels;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountProvider.Functions
{
    public class SignIn
    {
        private readonly ILogger<SignIn> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;
        private readonly IConfiguration _configuration;


        public SignIn(ILogger<SignIn> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> singInManager, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _singInManager = singInManager;
            _configuration = configuration;
        }

        [Function("SignIn")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();

            if (body != null)
            {
                var usr = JsonConvert.DeserializeObject<UserSignInRequest>(body);

                if (usr != null && usr.Email != null && usr.Password != null) 
                {
                    try
                    {
                        //var result = await _singInManager.PasswordSignInAsync(usr!.Email, usr.Password, usr.IsPersistent, false);

                        var result = await _singInManager.PasswordSignInAsync(usr.Email, usr.Password, false, false);

                        if (result.Succeeded)
                        {
                            var user = await _userManager.FindByEmailAsync(usr.Email);
                            var token = GenerateJwtToken(user);

                            return new OkObjectResult(token);
                        }
                    }
                    catch (Exception ex) { return new BadRequestObjectResult(ex.Message); }
                }
            }
            return new BadRequestResult();
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["_jwtSecret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id), // Assuming user.Id is the unique identifier for the user
                                                         // You can add more claims here as needed
                }),
                Expires = DateTime.UtcNow.AddDays(1), // Token expires in 7 days (adjust as needed)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
