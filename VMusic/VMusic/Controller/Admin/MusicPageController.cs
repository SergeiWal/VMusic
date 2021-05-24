using System.IO;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.Controller.Admin
{
    class MusicPageController
    {
        private UnitOfWork dbWorker;

        public MusicPageController()
        {
            dbWorker = new UnitOfWork();
        }

        public void DeleteSong(int id)
        {
            dbWorker.Songs.Delete(id);
            dbWorker.Save();
        }

        public void DeleteFromLocalFileRep(Song song)
        {
            FileInfo fileInfo = new FileInfo(song.Source);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }
    }
}
