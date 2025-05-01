using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Shared.DataTransferObject.IdentityDTOS;

namespace ServiceAbstraction
{
    public interface IAuthenticationcService
    {
        //Login 
        //This EndPoint Will Handle User Login Take Email and Password Then Return Token,
        //Email and DisplayName To Client
        Task<UserDTo> LoginAsync(LoginDTo loginDto);
        // Register
        //This EndPoint Will Handle User Registration Will Take Email , Password  , UserName , Display Name And Phone Number Then
        //Return Token, Email and Display Name To Client
        Task<UserDTo> RegisterAsync(RegisterDto registerDto);

        // Check Email
        // Take string Email Then Check If It Exist In The Database
        Task<bool> CheckEmailAsync(string email);


        // Get  Current User  Address
        // Take  string Email Then  Return AddressDTo 
        Task<AddressDTo> GetGurrentUserAddressAsync(string email);
        // Update Current User Address
        // Take AddressDTo Updated Address and string Email Then Return  AddressDTo after Update To Client  
        Task<AddressDTo> UpdateCurrentUserAddressAsync(AddressDTo addressDto, string email);
        // Get Current User
        //Take string Email Then Return UserDTo Token , Email and Display Name To Client
        Task<UserDTo> GetCurrentUserAsync(string email);



    }
}
