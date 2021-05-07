using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Models;
using VMusic.Repository;
using VMusic.ViewModels.Admin;

namespace VMusic.ViewModels.Client
{
    class TopSongListViewModel: BaseViewModel
    {
        private UnitOfWork dbWorker;
        private Playlist topPlaylist;
        private SongViewModel currentSong;
        public ObservableCollection<SongViewModel> LocalSongList { get; set;}

        public TopSongListViewModel()
        {
            dbWorker = new UnitOfWork();
            topPlaylist = dbWorker.Playlist.GetByPredicate(p => p.Name == TopMusicPageViewModel.TOP_LIST_NAME);
            LocalSongList = new ObservableCollection<SongViewModel>(dbWorker.Songs.GetAllObject().Select(s=>new SongViewModel(s)));
        }

        public SongViewModel CurrentSong
        {
            get => currentSong;
            set
            {
                currentSong = value;
                OnPropertyChanged("CurrentSong");
            }
        }
    }
}
