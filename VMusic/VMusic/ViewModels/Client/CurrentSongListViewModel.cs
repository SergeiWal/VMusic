using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Controller.Client.Player;
using VMusic.Repository;

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
