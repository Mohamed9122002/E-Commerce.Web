using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
    public class AuthenticationcService(UserManager<ApplicationUser> _userManager) : IAuthenticationcService
    {
        public async Task<UserDTo> LoginAsync(LoginDTo loginDto)
        {
            // Check if Email Is Exists 
            var User = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
            // Check Password 
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordValid)
            {
                // Return UserDto 
                return new UserDTo()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = CreateTokenAsync(User),
                };
            }
            else
            {
                throw new UnauthorizedException();

            }
        }



        public async Task<UserDTo> RegisterAsync(RegisterDto registerDto)
        {
            // Mapping Register Dto => ApplicationDto 
            var User = new ApplicationUser()
            {
                DisplayName = registerDto.DsiplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName
            };
            // Create User [ApplicationUser]
            var Result = await _userManager.CreateAsync(User, registerDto.Password);
            if (Result.Succeeded)
            {
                // Return UserDto 
                return new UserDTo()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(x => x.Description).ToList();
                // Throw BadRequest Exceptions 
                throw new BadRequestException(Errors);
            }



        }

        private string CreateTokenAsync(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }
    }
}
