using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Commands;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Admin
{
    class TopMusicPageViewModel: BaseViewModel
    {
        public static int TOP_LIST_SIZE = 10;
        public static string TOP_LIST_NAME = "BestMusic";

        private UnitOfWork dbWorker;
        private Playlist topSongPlaylist;
        public ObservableCollection<SongViewModel> TopSongList { get; set; }

        public TopMusicPageViewModel()
        {
            dbWorker = new UnitOfWork();

            TopSongList = new ObservableCollection<SongViewModel>();
            FillTopSongList();

            topSongPlaylist = new Playlist()
            {
                Name = TOP_LIST_NAME,
                Image = null,
                User = null,
                UserId = null
            };
        }


        private Command updateTopPlaylist;

        public Command UpdateTopPlaylist
        {
            get
            {
                return updateTopPlaylist ?? (updateTopPlaylist = new Command((obj) =>
                {
                    var playlist = dbWorker.Playlist.GetByPredicate((b) => b.Name == TOP_LIST_NAME && b.UserId == null);
                    topSongPlaylist.Songs = new List<Song>(TopSongList.Select(b => b.song));
                    if (playlist == null)
                    {
                        dbWorker.Playlist.Create(topSongPlaylist);
                        dbWorker.Save();
                    }
                    else
                    {
                        dbWorker.Playlist.Update(playlist, topSongPlaylist);
                        dbWorker.Save();
                    }
                }));
            }
        }


        private void FillTopSongList()
        {
            var songs = new ObservableCollection<SongViewModel>(dbWorker.Songs.GetAllObject().OrderBy(n => n.Rating)
                .Select(s => new SongViewModel(s)));
            for (int i = 0; i < TOP_LIST_SIZE && i<songs.Count; ++i)
            {
                TopSongList.Add(songs[i]);
            }
        }
    }
}
