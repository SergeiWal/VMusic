using System.Collections.ObjectModel;
using VMusic.Controller.Client.Player;

namespace VMusic.ViewModels.Client
{
    class CurrentSongListViewModel: BaseViewModel
    {
        private Player player;
        private SongViewModel currentSong;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }

        public CurrentSongListViewModel(Player player)
        {
            this.player = player;
            LocalSongList =
                new ObservableCollection<SongViewModel>(player.CurrentPlaylist);
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
