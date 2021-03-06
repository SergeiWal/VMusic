using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Models;

namespace VMusic.Repository
{
    class SongRepository: IRepository<Song>
    {
        private VMusicContext db;

        public SongRepository(VMusicContext db)
        {
            this.db = db;
        }

        public IEnumerable<Song> GetAllObject()
        {
            return db.Songs;
        }

        public Song GetById(int id)
        {
            return db.Songs.Find(id);
        }

        public IEnumerable<Song> GetByName(string name)
        {
            return db.Songs.Where(s => s.Name.Contains(name));
        }

        public void Create(Song obj)
        {
            db.Songs.Add(obj);
        }

        public void Update(Song oldObj, Song newObj)
        {
            throw new NotImplementedException();
        }

        public void RatingUpdate(Song obj)
        {
            obj.Rating++;
        }

        public void Delete(int id)
        {
            Song song = db.Songs.Find(id);
            if (song != null)
            {
                db.Songs.Remove(song);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
