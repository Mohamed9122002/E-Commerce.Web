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
    }
}
