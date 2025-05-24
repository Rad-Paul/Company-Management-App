using AutoMapper;
using CompanyManagementAPI.DTOs;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Entities.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CompanyManagementAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User? _user;

        public AuthenticationService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            User user = _mapper.Map<User>(userForRegistration);

            IdentityResult result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);
            bool result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

            if (!result)
                throw new UserNotFoundException();

            return result;
        }

        public async Task<string> CreateToken()
        {
            SigningCredentials signingCredentials = GetSigningCredentials();
            List<Claim> claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            byte[] key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("CESECRET")!);
            SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(key);

            return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user!.UserName!)
            };

            IEnumerable<string> roles = await _userManager.GetRolesAsync(_user);

            foreach(string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            IConfiguration jwtSettings = _configuration.GetSection("JwtSettings");

            JwtSecurityToken tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}
