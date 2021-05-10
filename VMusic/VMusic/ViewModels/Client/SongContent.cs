using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


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

        public bool Next()
        {
            int i = CurrentPlaylist.IndexOf(CurrentSong);
            ++i;
            if (i < CurrentPlaylist.Count)
            {
                CurrentSong = CurrentPlaylist[i];
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Prev()
        {
            int i = CurrentPlaylist.IndexOf(CurrentSong);
            --i;
            if (i >= 0)
            {
                CurrentSong = CurrentPlaylist[i];
                return true;
            }
            else
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
