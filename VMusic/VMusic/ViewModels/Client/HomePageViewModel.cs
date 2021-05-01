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
        private SongRepository songRepository;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }

        public HomePageViewModel(SongContent songContent)
        {
            songRepository = new SongRepository();
            this.songContent = songContent;
            LocalSongList =
                new ObservableCollection<SongViewModel>(songRepository.GetAllObject()
                    .Select(o => new SongViewModel(o)));
        }

        public SongViewModel CurrentSong
        {
            get => currentSong;
            set
            {
                currentSong = value;
                songContent.CurrentSong = value;
                //MessageBox.Show(songContent.CurrentSong.Name);
                OnPropertyChanged("CurrentSong");
            }
        }

    }
}
