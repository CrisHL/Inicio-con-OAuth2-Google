using OAuth2.Models;
using OAuth2.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace OAuth2.Services
{
    public class UserService
    {
        public async Task<bool> ValidateExistingUser(string username)
        {
            try
            {
                using (var db = new OAuth2Entities())
                {
                    return await db.User.AnyAsync(u => u.UserName == username);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ValidateExistingEmail(string email)
        {
            try
            {
                using (var db = new OAuth2Entities())
                {
                    return await db.User.AnyAsync(u => u.Email == email);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Saveuser(UserVM userVM)
        {
            try
            {
                using (var db = new OAuth2Entities())
                {
                    var newAddress = new Address
                    {
                        Country = userVM.AddressVM.Country,
                        State = userVM.AddressVM.State,
                        City = userVM.AddressVM.City,
                        Street = userVM.AddressVM.Street,
                        ZipCode = userVM.AddressVM.ZipCode,
                    };

                    var newUser = new User
                    {
                        Email = userVM.Email,
                        Password = Crypto.SHA256(userVM.Password),
                        PhoneNumber = userVM.PhoneNumber,
                        UserName = userVM.UserName,
                        Address = newAddress
                    };

                    db.User.Add(newUser);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            try
            {
                string passwordHash = Crypto.SHA256(password);
                using (var db = new OAuth2Entities())
                {
                    return await db.User.AnyAsync(u => u.UserName == username && u.Password == passwordHash);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}