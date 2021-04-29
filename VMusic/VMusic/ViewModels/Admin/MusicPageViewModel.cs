using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMusic.Commands;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Admin
{
    class MusicPageViewModel: BaseViewModel
    {
        private SongRepository repository;
        public ObservableCollection<SongViewModel> Songs { get; set; }

        private SongViewModel selectedSong = null;

        public MusicPageViewModel(ObservableCollection<SongViewModel> songs)
        {
            repository = new SongRepository();
            Songs = songs;
        }

        public SongViewModel SelectedSong
        {
            get => selectedSong;
            set
            {
                selectedSong = value;
                OnPropertyChanged("SelectedSong");
            }
        }

        private Command deleteSong;

        public Command DeleteSong
        {
            get
            {
                return deleteSong ?? (deleteSong = new Command((obj) =>
                {
                    SongViewModel song = obj as SongViewModel;
                    if (song != null)
                    {
                        SelectedSong = null;
                        repository.Delete(song.Id);
                        repository.Save();
                        Songs.Remove(song);
                    }
                }));
            }
        }
    }
}
