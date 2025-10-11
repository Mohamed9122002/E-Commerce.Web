using Shared.DTOS.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        // Login 
        Task<UserDto> LoginAsync(LoginDto loginDto);

        // Register 
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        // Check Email 
        Task<bool> CheckEmailAsync(string Email);
        // Get Current User Address 
        Task<AddressDto> GetCurrentUserAddressAsync(string Email);
        // Updated Current User Address 
        Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto);
        Task<UserDto> GetCurrentUserAsync(string Email);
    }
}
