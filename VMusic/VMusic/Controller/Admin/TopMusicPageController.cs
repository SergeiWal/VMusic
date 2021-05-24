using System.Collections.Generic;
using System.Linq;
using VMusic.Models;
using VMusic.Repository;
using VMusic.ViewModels;

namespace VMusic.Controller.Admin
{
    class TopMusicPageController
    {
        public static int TOP_LIST_SIZE = 10;
        public static string TOP_LIST_NAME = "BestMusic";

        private UnitOfWork dbWorker;

        public TopMusicPageController()
        {
            dbWorker = new UnitOfWork();
        }

        public IEnumerable<Song> GetSongsSortedByRating()
        {
            return dbWorker.Songs.GetAllObject().OrderByDescending(n => n.Rating);
        }

        public Playlist GetLikeSongList()
        {
            return dbWorker.Playlist.GetByPredicate((b) => b.Name == TOP_LIST_NAME && b.UserId == null);
        }

        public void CreateLikeSongList(Playlist playlist)
        {
            dbWorker.Playlist.Create(playlist);
            dbWorker.Save();
        }

        public void AddSongs(IEnumerable<SongViewModel> topSongList)
        {
            var playlist = dbWorker.Playlist.GetAllObject().FirstOrDefault(p => p.Name == TOP_LIST_NAME);

            if (playlist != null)
            {
                dbWorker.Playlist.ClearSongs(playlist.Id);
                foreach (var song in topSongList)
                {
                    playlist.Songs.Add(song.song);
                }
                dbWorker.Save();
            }
        }
    }
}
