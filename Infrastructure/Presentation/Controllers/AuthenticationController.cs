using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

   public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Login 
        [HttpPost("Login")] //Post/BaseUrl/api/Authentication/Login
        public async Task<ActionResult<UserDTo>> Login(LoginDTo loginDTo)
        {
            var User = await _serviceManager.AuthenticationcService.LoginAsync(loginDTo);
            return Ok(User);
        }
        // Register 
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTo>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.AuthenticationcService.RegisterAsync(registerDto);
            return Ok(User);
        }

        // Check Email
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = await _serviceManager.AuthenticationcService.CheckEmailAsync(email);
            return Ok(Result);
        }
        // Get Current User
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTo>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser = await _serviceManager.AuthenticationcService.GetCurrentUserAsync(Email!);
            return Ok(AppUser);
        }
        // Get Current User Address 
        [Authorize]
        [HttpGet("CurrentUserAddress")]
        public async Task<ActionResult<AddressDTo>> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviceManager.AuthenticationcService.GetGurrentUserAddressAsync(Email!);
            return Ok(Address);
        }
        // Update Current User Address
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTo>> UpdateCurrentUserAddress(AddressDTo addressDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var UpdateAddress = await _serviceManager.AuthenticationcService.UpdateCurrentUserAddressAsync(addressDto, Email!);
            return Ok(UpdateAddress);
        }

    }
}
