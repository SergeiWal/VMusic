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

        private SongRepository songRepository;
        private PlaylistRepository playlistRepository;
        private Playlist topSongPlaylist;
        public ObservableCollection<SongViewModel> TopSongList { get; set; }

        public TopMusicPageViewModel()
        {
            songRepository = new SongRepository();
            playlistRepository = new PlaylistRepository();

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
                    var playlist = playlistRepository.GetByPredicate((b) => b.Name == TOP_LIST_NAME && b.UserId == null);
                    topSongPlaylist.Songs = new List<Song>(TopSongList.Select(b => b.song));
                    if (playlist == null)
                    {
                        playlistRepository.Create(topSongPlaylist);
                        playlistRepository.Save();
                    }
                    else
                    {
                        playlistRepository.Update(playlist, topSongPlaylist);
                        playlistRepository.Save();
                    }
                }));
            }
        }


        private void FillTopSongList()
        {
            var songs = new ObservableCollection<SongViewModel>(songRepository.GetAllObject().OrderBy(n => n.Rating)
                .Select(s => new SongViewModel(s)));
            for (int i = 0; i < TOP_LIST_SIZE && i<songs.Count; ++i)
            {
                TopSongList.Add(songs[i]);
            }
        }
    }
}
