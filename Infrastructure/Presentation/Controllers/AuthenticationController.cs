using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

   public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Login 
        [HttpPost("Login")] //Post/BaseUrl/api/Authentication/Login
        public async Task<ActionResult<UserDTo>> Login (LoginDTo loginDTo)
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

    }
}
