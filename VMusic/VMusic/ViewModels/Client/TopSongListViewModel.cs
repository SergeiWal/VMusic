using System.Collections.ObjectModel;
using System.Linq;
using VMusic.Controller.Admin;
using VMusic.Controller.Client.Player;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class TopSongListViewModel: BaseViewModel
    {
        private Player player;
        private Playlist topPlaylist;
        private SongViewModel currentSong;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> LocalSongList { get; set;}

        public TopSongListViewModel(Player player)
        {
            this.player = player;
            topPlaylist = (new UnitOfWork()).Playlist.GetByPredicate(p => p.Name == TopMusicPageController.TOP_LIST_NAME);
            LocalSongList = new ObservableCollection<SongViewModel>(topPlaylist.Songs.OrderByDescending(s=>s.Rating).
                Select(s=>new SongViewModel(s){Index = ++itemCount}));
        }

        public SongViewModel CurrentSong
        {
            get => currentSong;
            set
            {
                currentSong = value;
                player.CurrentPlaylist = LocalSongList;
                player.CurrentSong = value;
                OnPropertyChanged("CurrentSong");
            }
        }
    }
}
