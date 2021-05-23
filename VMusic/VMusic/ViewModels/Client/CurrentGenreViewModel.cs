using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Controller.Client.Player;
using VMusic.Converters;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class CurrentGenreViewModel : BaseViewModel
    {
        private PlaylistViewModel currentGenre;
        private SongViewModel currentSong;
        private Player player;
        private UnitOfWork dbWorker;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> Songs { get; set; }

        public CurrentGenreViewModel(PlaylistViewModel currentGenre, Player player)
        {
            dbWorker = new UnitOfWork();
            this.currentGenre = currentGenre;
            this.player = player;
            Songs = new ObservableCollection<SongViewModel>(dbWorker.Songs.GetAllObject()
                .Where(s => s.Genre == GenreConverter.StringToGenre(currentGenre.Name))
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

    }
}
