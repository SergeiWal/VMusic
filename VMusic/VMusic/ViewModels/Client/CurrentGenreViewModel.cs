using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Converters;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class CurrentGenreViewModel : BaseViewModel
    {
        private PlaylistViewModel currentGenre;
        private SongViewModel currentSong;
        private SongContent songContent;
        private UnitOfWork dbWorker;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> Songs { get; set; }

        public CurrentGenreViewModel(PlaylistViewModel currentGenre, SongContent songContent)
        {
            dbWorker = new UnitOfWork();
            this.currentGenre = currentGenre;
            this.songContent = songContent;
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
                songContent.CurrentSong = currentSong;
                songContent.CurrentPlaylist = Songs;
                OnPropertyChanged("CurrentSong");
            }
        }

    }
}
