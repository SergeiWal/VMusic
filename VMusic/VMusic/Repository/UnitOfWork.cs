using System;
using VMusic.Models;

namespace VMusic.Repository
{
    class UnitOfWork: IDisposable
    {
        private VMusicContext db = new VMusicContext();
        private PlaylistRepository playlistRepository;
        private SongRepository songRepository;
        private UserRepository userRepository;


        public PlaylistRepository Playlist
        {
            get
            {
                if (playlistRepository == null)
                {
                    playlistRepository = new PlaylistRepository(db);
                }

                return playlistRepository;
            }
        }

        public SongRepository Songs
        {
            get
            {
                if (songRepository == null)
                {
                    songRepository = new SongRepository(db);
                }

                return songRepository;
            }
        }

        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }

                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
