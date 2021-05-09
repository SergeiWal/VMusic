using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class CurrentSongListViewModel: BaseViewModel
    {
        private SongContent songContent;
        private SongViewModel currentSong;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }

        public CurrentSongListViewModel(SongContent songContent)
        {
            this.songContent = songContent;
            LocalSongList =
                new ObservableCollection<SongViewModel>(songContent.CurrentPlaylist);
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
