using System.Collections.Generic;
using System.Linq;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.Controller.Admin
{
    class AdminMainController
    {
        private UnitOfWork dbWorker;

        public AdminMainController()
        {
            dbWorker = new UnitOfWork();
        }

        public IEnumerable<Song> GetSongs()
        {
            return dbWorker.Songs.GetAllObject();
        }

        public IEnumerable<User> GetUser()
        {
            return dbWorker.Users.GetAllObject().Where(u => !u.IsAdmin);
        }
    }
}
