using System.Linq;
using VMusic.Models;
using VMusic.Repository;
using VMusic.ViewModels;

namespace VMusic.Controller.Client.LikeSongsController
{
    class LikeSongsController
    {
        public static string LIKE_SONG_LIST_NAME = "Избраное";

        private User user;
        private UnitOfWork dbWorker;

        public LikeSongsController(User user)
        {
            this.user = user;
            dbWorker = new UnitOfWork();
        }


        public bool IsHasLikeSongList()
        {
            var plist = dbWorker.Playlist.GetByPredicate(p => p.UserId == user.Id && p.Name == LIKE_SONG_LIST_NAME);
            return plist != null;
        }

        public Playlist CreateLikeSongList()
        {
            Playlist playlist = new Playlist()
            {
                Name = LIKE_SONG_LIST_NAME,
                UserId = user.Id
            };
            dbWorker.Playlist.Create(playlist);
            dbWorker.Save();
            return playlist;
        }

        public bool AddLikeSongInList(SongViewModel currentSong)
        {
            var playlist = dbWorker.Playlist.GetByPredicate(p => p.UserId == user.Id && p.Name == LIKE_SONG_LIST_NAME);
            var song = dbWorker.Songs.GetById(currentSong.Id);
            var songFromList = playlist.Songs.FirstOrDefault(s => s.Id == currentSong.Id);
            if (song != songFromList)
            {
                playlist.Songs.Add(song);
                dbWorker.Save();
                SongRatingUp(currentSong);
                return true;
            }

            return false;
        }

        private void SongRatingUp(SongViewModel currentSong)
        {
            var song = dbWorker.Songs.GetById(currentSong.Id);
            dbWorker.Songs.RatingUpdate(song);
            dbWorker.Save();
        }
    }
}
