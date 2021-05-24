using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VMusic.Commands;
using VMusic.Controller.Admin;
using VMusic.Models;

namespace VMusic.ViewModels.Admin
{
    class TopMusicPageViewModel: BaseViewModel
    {
        private TopMusicPageController controller;
        private Playlist topSongPlaylist;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> TopSongList { get; set; }

        public TopMusicPageViewModel()
        {
            controller = new TopMusicPageController();

            TopSongList = new ObservableCollection<SongViewModel>(controller.GetLikeSongList().Songs
                .Select(s => new SongViewModel(s){Index = ++itemCount}));

            topSongPlaylist = new Playlist()
            {
                Name = TopMusicPageController.TOP_LIST_NAME,
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
                    var playlist = controller.GetLikeSongList();
                    topSongPlaylist.Songs = controller.GetSongsSortedByRating().ToList();
                    if (playlist == null)
                    {
                        controller.CreateLikeSongList(topSongPlaylist);
                    }

                    UpdateLocalSongList(topSongPlaylist.Songs);
                    controller.AddSongs(topSongPlaylist.Songs);
                }));
            }
        }

        private void UpdateLocalSongList(IEnumerable<Song> likeSongs)
        {
            TopSongList.Clear();
            itemCount = 0;
            foreach (var c in likeSongs)
            {
                TopSongList.Add(new SongViewModel(c){Index = ++itemCount});
            }
        }
    }
}
