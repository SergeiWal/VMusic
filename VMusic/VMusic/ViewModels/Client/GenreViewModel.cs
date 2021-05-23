using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class GenreViewModel: BaseViewModel
    {
        private PlaylistViewModel selectedGenre;
        public ObservableCollection<PlaylistViewModel> GenreList { get; set; }

        public GenreViewModel()
        {
            GenreList = new ObservableCollection<PlaylistViewModel>(GetGenreList().Select(p=>new PlaylistViewModel(p)));
        }

        public PlaylistViewModel SelectedGenre
        {
            get => selectedGenre;
            set
            {
                selectedGenre = value;
                OnPropertyChanged("SelectedGenre");
            }
        }

        private List<Playlist> GetGenreList()
        {
            
            var songs = (new UnitOfWork()).Songs.GetAllObject();
            Playlist rock = new Playlist()
            {
                Name = "Рок"
            };
            Playlist pop = new Playlist()
            {
                Name = "Поп"
            };
            Playlist folk = new Playlist()
            {
                Name = "Фолк"
            };
            Playlist rap = new Playlist()
            {
                Name = "Реп"
            };
            Playlist indi = new Playlist()
            {
                Name = "Инди"
            };
            Playlist jazz = new Playlist()
            {
                Name = "Джаз"
            };
            Playlist classic = new Playlist()
            {
                Name = "Классика"
            };
            List<Playlist> playlistList = new List<Playlist>(){rock,rap,pop,folk,indi,jazz,classic};
            return playlistList;
        }

    }
}
