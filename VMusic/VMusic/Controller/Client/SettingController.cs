using VMusic.Hasher;
using VMusic.Repository;

namespace VMusic.Controller.Client
{

    class SettingController
    {
        private UnitOfWork dbWorker;

        public SettingController()
        {
            dbWorker = new UnitOfWork();
        }

        public void ChangePassword(int userId, string password)
        {
            var user = dbWorker.Users.GetById(userId);
            if (user != null)
            {
                user.Password = PasswordHasher.GetHash(password);
                dbWorker.Save();
            }
        }

        public bool DeleteUser(int userId)
        {
            var user = dbWorker.Users.GetById(userId);
            if (user != null)
            {
                if (!user.IsAdmin)
                {
                    dbWorker.Users.Delete(user.Id);
                    dbWorker.Save();
                    return true;
                }
            }

            return false;
        }
    }
}

