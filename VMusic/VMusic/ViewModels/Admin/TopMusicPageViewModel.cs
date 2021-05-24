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

            TopSongList = new ObservableCollection<SongViewModel>(controller.GetSongsSortedByRating()
                .Select(s => new SongViewModel(s){Index = ++itemCount}).Take(TopMusicPageController.TOP_LIST_SIZE));

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
                    topSongPlaylist.Songs = new List<Song>(TopSongList.Select(b => b.song));
                    if (playlist == null)
                    {
                        controller.CreateLikeSongList(topSongPlaylist);
                    }

                    controller.AddSongs(TopSongList);
                }));
            }
        }
    }
}
