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
    class FindSongViewModel: BaseViewModel
    {
        private Player player;
        private SongViewModel currentSong;
        private UnitOfWork dbWorker;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }


        public FindSongViewModel(string findSongString, Player player)
        {
            dbWorker = new UnitOfWork();
            this.player = player;
            LocalSongList = new ObservableCollection<SongViewModel>(dbWorker.Songs.GetAllObject()
                .Where(s=>s.Name.Contains(findSongString) || s.Author.Contains(findSongString) || s.Album.Contains(findSongString))
                .Select(s=>new SongViewModel(s){Index = ++itemCount}));
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
