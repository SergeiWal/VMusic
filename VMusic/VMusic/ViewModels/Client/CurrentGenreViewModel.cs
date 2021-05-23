using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VMusic.Controller.Client.Player;
using VMusic.Converters;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class CurrentGenreViewModel : BaseViewModel
    {
        private PlaylistViewModel currentGenre;
        private SongViewModel currentSong;
        private Player player;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> Songs { get; set; }

        public CurrentGenreViewModel(PlaylistViewModel currentGenre, Player player)
        {
            this.currentGenre = currentGenre;
            this.player = player;
            Songs = new ObservableCollection<SongViewModel>(GetSongs()
                .Select(s => new SongViewModel(s){Index = ++itemCount}));
        }

        public string Name
        {
            get => currentGenre.Name;
        }

        public SongViewModel CurrentSong
        {
            get => currentSong;
            set
            {
                currentSong = value;
                player.CurrentSong = currentSong;
                player.CurrentPlaylist = Songs;
                OnPropertyChanged("CurrentSong");
            }
        }

        private IEnumerable<Song> GetSongs()
        {
            UnitOfWork dbWorker = new UnitOfWork();
            return dbWorker.Songs.GetAllObject()
                .Where(s => s.Genre == GenreConverter.StringToGenre(currentGenre.Name));
        } 

    }
}
