using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data
{
  public class AdminRepository
    {
      private string _connString;
      public AdminRepository(string connString)
      {
          _connString = connString;
      }
        public void SignUp(string firstname, string lastname, string username, string password)
        {
            string salt = PasswordHelper.GenerateSalt();
            string hash = PasswordHelper.HashPassword(password, salt);
            ECommerceDataContext context = new ECommerceDataContext();
            context.AdminUsers.InsertOnSubmit(new AdminUser
            {
                FirstName = firstname,
                LastName = lastname,
                UserName = username,
                PasswordHash = hash,
                PasswordSalt = salt
            });
            context.SubmitChanges();
        }
        public AdminUser LogIn(string username, string password)
        {
         ECommerceDataContext context = new ECommerceDataContext();
         AdminUser admin = context.AdminUsers.First(au => au.UserName == username);
            if (admin == null)
            {
                return null;
            }
            if (PasswordHelper.isMatch(password, admin.PasswordHash, admin.PasswordSalt))
            {
                     return admin;
             }
         return null;
        }
    }
}