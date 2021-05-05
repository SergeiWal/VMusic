using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VMusic.Hasher;

namespace VMusic.Models
{
    class VMusicContextInitializer: DropCreateDatabaseIfModelChanges<VMusicContext>
    {
        protected override void Seed(VMusicContext context)
        {
            User admin = new User()
            {
                Name = "Admin",
                Login = "Admin",
                Password = PasswordHasher.GetHash("Admin"),
                IsAdmin = true,
                IsBlocked = false
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}
