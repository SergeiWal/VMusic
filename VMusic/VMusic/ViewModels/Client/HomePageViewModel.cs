using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VMusic.Controller.Client.Player;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class HomePageViewModel: BaseViewModel
    {
        private Player player;
        private SongViewModel currentSong;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }

        public HomePageViewModel(Player player)
        {
            this.player = player;
            LocalSongList =
                new ObservableCollection<SongViewModel>(GetSongs()
                    .Select(o => new SongViewModel(o){Index = ++itemCount}));
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

        private IEnumerable<Song> GetSongs()
        {
            return (new UnitOfWork()).Songs.GetAllObject();
        }

    }
}
