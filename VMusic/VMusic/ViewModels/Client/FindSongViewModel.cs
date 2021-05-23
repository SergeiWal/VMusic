using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VMusic.Controller.Client.Player;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class FindSongViewModel: BaseViewModel
    {
        private Player player;
        private SongViewModel currentSong;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }


        public FindSongViewModel(string findSongString, Player player)
        {
            this.player = player;
            LocalSongList = new ObservableCollection<SongViewModel>(GetSongs(findSongString)
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

        private IEnumerable<Song> GetSongs(string findSongString)
        {
            UnitOfWork dbWorker = new UnitOfWork();
            return dbWorker.Songs.GetAllObject()
                .Where(s => s.Name.Contains(findSongString) || s.Author.Contains(findSongString) ||
                            s.Album.Contains(findSongString));
        } 
    }
}
