using AutoMapper;
using Entities.DataTransferObjects.Auth;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        private User? _user;
        public AuthenticationManager(
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            // check is role exist in db
            var roles = await _roleManager.Roles.ToListAsync();
            var normalizedNames = roles.Select(r =>r.NormalizedName).ToList();
            var role = userForRegistrationDto.Role.ToUpperInvariant();
            if (!normalizedNames.Contains(role))
                throw new Exception($"Role {role} does not exist");
            else
                userForRegistrationDto.Role = roles.FirstOrDefault(r => r.NormalizedName == role).Name;

            // check is user exist in db
            if (await _userManager.FindByNameAsync(userForRegistrationDto.UserName) != null)
                throw new Exception($"User {userForRegistrationDto.UserName} is already registered");

            var user = _mapper.Map<User>(userForRegistrationDto);

            var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, userForRegistrationDto.Role);
            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto)
        {
            _user = await _userManager.FindByNameAsync(userForAuthenticationDto.Username);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthenticationDto.Password));

            if (!result)
                throw new Exception("Username or password is incorrect");
            return result;
        }
    }
}
