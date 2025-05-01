using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
    public class AuthenticationcService(UserManager<ApplicationUser> _userManager,IMapper _mapper ,IConfiguration _configuration) : IAuthenticationcService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            // Check If Email Exist In The Database 
            var User = await _userManager.FindByEmailAsync(email);
            return User is not null;

        }

        public async Task<UserDTo> GetCurrentUserAsync(string email)
        {
           var User = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDTo()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await CreateTokenAsync(User)
            };
        }

        public async Task<AddressDTo> GetGurrentUserAddressAsync(string email)
        {
            var User = await _userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.Email ==  email) ?? throw new UserNotFoundException(email);
            if (User.Address is not  null)
            {
                return _mapper.Map<Address, AddressDTo>(User.Address);
            }else
            {
                throw new AddressNotFoundExpection(User.UserName);
            }
        }
        public async Task<AddressDTo> UpdateCurrentUserAddressAsync(AddressDTo addressDto, string email)
        {
            var User = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email) ?? throw new UserNotFoundException(email);
            if(User is not null)
            {
                // Update Address
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
                User.Address.Street = addressDto.Street;
            }
            else
            {
                // Mapp AddressDto To Address
                 User.Address = _mapper.Map<AddressDTo, Address>(addressDto);

            }
           await  _userManager.UpdateAsync(User);
            return _mapper.Map<AddressDTo>(User.Address);
        }

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
                    Token =  await CreateTokenAsync(User),
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
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(x => x.Description).ToList();
                // Throw BadRequest Exceptions 
                throw new BadRequestException(Errors);
            }



        }



        private async Task<string> CreateTokenAsync(ApplicationUser applicationUser)
        {
            // 
            // PlayLoad
            var Claims = new List<Claim>()
            {               
                new Claim(ClaimTypes.Email, applicationUser.Email!),
                new Claim(ClaimTypes.Name, applicationUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
            };
            var Roles = await _userManager.GetRolesAsync(applicationUser);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // Signing 
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds
            );
            // Return Token
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
