using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.ViewModels.Client
{
    class SongContent: ISongContent
    {

        private SongViewModel currentSong;
        private ObservableCollection<SongViewModel> currentPlaylist;

        public SongViewModel CurrentSong
        {
            get => currentSong;
            set
            {
                currentSong = value;
                OnPropertyChanged("CurrentSong");
            }
        }

        public ObservableCollection<SongViewModel> CurrentPlaylist
        {
            get => currentPlaylist;
            set
            {
                currentPlaylist = value;
                OnPropertyChanged("CurrentPlaylist");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
