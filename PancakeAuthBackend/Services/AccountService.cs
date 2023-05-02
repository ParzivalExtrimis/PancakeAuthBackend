using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PancakeAuthBackend.Services {
    public class AccountService : IAccountService {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AccountService> _logger;
        private User? _user;

        public AccountService(UserManager<User> userManager, IConfiguration config, ILogger<AccountService> logger) {
            _config = config;
            _userManager = userManager;
            _logger = logger;
        }

        async Task<string> IAccountService.GetToken() {
            var credentials = GetCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenSignature(credentials, claims);

            _logger.LogInformation("Token-Generator", "Token Generation Complete");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateTokenSignature(SigningCredentials credentials, List<Claim> claims) {
            var jwtSettings = _config.GetSection("JWT");

            _logger.LogInformation("Token-Generator", "Token Signed");
            return new JwtSecurityToken(
                issuer: jwtSettings.GetValue<string>("Issuer"),
                audience: jwtSettings.GetValue<string>("Audience"),
                expires: DateTime.UtcNow.AddHours(jwtSettings.GetValue<double>("Lifetime")),
                signingCredentials: credentials,
                claims: claims
            );
        }

        private async Task<List<Claim>> GetClaims() {
            if (_user is null) {
                return new List<Claim>();
            }
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach(var role in roles) {
                claims.Add(
                    new Claim(ClaimTypes.Role, role)
                );
            }
            _logger.LogInformation("Token-Generator", "Claims Added");
            return claims;
        }

        private SigningCredentials GetCredentials() {
            var key = _config.GetSection("JWT").GetValue<string>("Key");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            _logger.LogInformation("Token-Generator", "Credentials Added");
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        async Task<bool> IAccountService.SignIn(LoginDTO loginDetails) {
            _logger.LogInformation("SignIn", $"Sign-In attempted : {loginDetails.UserName}");
            _user = await _userManager.FindByNameAsync(loginDetails.UserName);
            var validLogin = _user != null && await _userManager.CheckPasswordAsync(_user, loginDetails.Password);

            if (validLogin) {
                _logger.LogInformation("SignIn", $"Successfully Signed-In : {loginDetails.UserName}");
                return true;
            }

            _logger.LogInformation("SignIn", $"Sign-In failed : {loginDetails.UserName}");
            return false;
        }
    }
}
