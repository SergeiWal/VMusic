using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class HomePageViewModel: BaseViewModel
    {
        private SongContent songContent;
        private SongViewModel currentSong;
        private UnitOfWork dbWorker;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }

        public HomePageViewModel(SongContent songContent)
        {
            dbWorker = new UnitOfWork();
            this.songContent = songContent;
            LocalSongList =
                new ObservableCollection<SongViewModel>(dbWorker.Songs.GetAllObject()
                    .Select(o => new SongViewModel(o)));
        }

        public SongViewModel CurrentSong
        {
            get => currentSong;
            set
            {
                currentSong = value;
                songContent.CurrentPlaylist = LocalSongList;
                songContent.CurrentSong = value;
                OnPropertyChanged("CurrentSong");
            }
        }

    }
}
