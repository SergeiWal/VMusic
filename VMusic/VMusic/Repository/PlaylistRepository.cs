using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Models;

namespace VMusic.Repository
{
    class PlaylistRepository: IRepository<Playlist>
    {
        private VMusicContext db;
        private bool disposed = false;

        public PlaylistRepository()
        {
            db = new VMusicContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Playlist> GetAllObject()
        {
            return db.Playlists;
        }

        public Playlist GetById(int id)
        {
            return db.Playlists.Find(id);
        }

        public Playlist GetByPredicate(Func<Playlist, bool> predicate)
        {
            return  db.Playlists.FirstOrDefault(predicate);
        }

        public void Create(Playlist obj)
        {
            db.Playlists.Add(obj);
        }

        public void Update(Playlist oldObj, Playlist newObj)
        {
            var dest = db.Playlists.Find(oldObj);
            dest.User = newObj.User;
            dest.Songs = newObj.Songs;
            dest.Image = newObj.Image;
            dest.Name = newObj.Name;
            dest.UserId = newObj.UserId;
        }

        public void Delete(int id)
        {
            var obj = db.Playlists.Find(id);
            if (obj != null)
            {
                db.Playlists.Remove(obj);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
