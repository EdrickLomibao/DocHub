using Microsoft.AspNet.Identity.Owin;
using DocHub.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocHub.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace DocHub.Services
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork unitOfWork;

        public AccountService(IUnitOfWork _unitOfWork) {
            unitOfWork = _unitOfWork;
        }

        public SignInStatus AuthenticateUser(string email, string password, ApplicationSignInManager SignInManager)
        {
            try
            {

                var result = SignInManager.PasswordSignIn(email, password, true, shouldLockout: false);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public ReturnStatus RegisterUser(string Email, string Password, string ConfirmPassword, string Firstname, string Lastname, ApplicationUserManager UserManager) {

            ReturnStatus returnStatus = new ReturnStatus();

            try {

                if (ComparePassword(Password, ConfirmPassword)
                    && ValidateEmail(Email)) {

                    ApplicationUser account = new ApplicationUser();

                    account.Email = Email;
                    account.UserName = Email;
                    account.Firstname = Firstname;
                    account.Lastname = Lastname;

                    var result = UserManager.CreateAsync(account, Password);

                    // EOL Editted with Error
                    if (result.Result.Succeeded)
                    {
                        returnStatus.Status = true;
                        returnStatus.Message = "Registration Successful.";
                    }
                    else
                    {
                        returnStatus.Status = result.Result.Succeeded;
                        returnStatus.Message = "Invalid Registration inputs.";
                    }

                    return returnStatus;

                }
                else {
                    returnStatus.Message = "Registration Failed.";
                    returnStatus.Status = false;

                    return returnStatus;
                }


            }
            catch (Exception ex) {

                returnStatus.Message = "Registration Failed.";
                returnStatus.Status = false;

                return returnStatus;
            }


        }

        private bool ComparePassword(string password_1, string passowrd_2)
        {
            return password_1.ToLower() == passowrd_2.ToLower();
        }

        private bool ValidateEmail(string email)
        {

            try
            {

                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;

            }
            catch
            {
                return false;
            }

        }

        private string EncryptPassword(string password)
        {

            MD5 hash = MD5.Create();

            string hashvalue = GetMd5Hash(hash, password);


            return hashvalue;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public bool VerifyMd5Hash(string input, string hash)
        {

            MD5 md5Hash = MD5.Create();

            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            var result = unitOfWork.UserRepository.SelectAll().ToList();
            return result;

        }

        //public ApplicationUser GetUserID(string userEmail)
        //{
        //    ApplicationUser user = repository.Find(o => o.Email == userEmail);

        //    return user;

        //}

        //public Task<ApplicationUser> GetUserIDAsync(string userEmail)
        //{
        //    ApplicationUser user = repository.Find(o => o.Email == userEmail);

        //    Thread.Sleep(5000);
        //    return Task.FromResult(user);

        //}

    }

}