using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using DocHub.Models;

namespace DocHub.Services
{
    public interface IAccountService
    {
        SignInStatus AuthenticateUser(string email, string password, ApplicationSignInManager SignInManager);
        ReturnStatus RegisterUser(string Email, string Password, string ConfirmPassword, string Firstname, string Lastname, ApplicationUserManager UserManager);
        IEnumerable<ApplicationUser> GetUsers();

    }

}