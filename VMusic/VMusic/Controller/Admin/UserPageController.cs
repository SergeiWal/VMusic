using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Repository;

namespace VMusic.Controller.Admin
{
    class UserPageController
    {
        private UnitOfWork dbWorker;

        public UserPageController()
        {
            dbWorker = new UnitOfWork();
        }

        public bool BlockingUser(int id)
        {
            var dbUser = dbWorker.Users.GetById(id);
            dbUser.IsBlocked = dbUser.IsBlocked != true;
            dbWorker.Save();
            return dbUser.IsBlocked;
        }

        public void TransferAdminStatus(int userId, int adminId)
        {
            var user = dbWorker.Users.GetById(userId);
            var admin = dbWorker.Users.GetById(adminId);
            user.IsAdmin = true;
            admin.IsAdmin = false;
            dbWorker.Save();
        }

        public void DeleteUserFromDb(int id)
        {
            dbWorker.Users.Delete(id);
            dbWorker.Save();
        }
    }
}
