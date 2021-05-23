using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VMusic.Models;

namespace VMusic.Repository
{
    class UserRepository: IRepository<User>
    {
        private VMusicContext db;


        public UserRepository(VMusicContext db)
        {
            this.db = db;
        }

        public IEnumerable<User> GetAllObject()
        {
            return db.Users;
        }

        public User GetById(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User obj)
        {
            db.Users.Add(obj);
        }

        public void Update(User oldObj, User newObj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            User user = db.Users.Include(u => u.Playlists).FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
