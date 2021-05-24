using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMusic.Commands;
using VMusic.Controller.Admin;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Admin
{
    class MusicPageViewModel: BaseViewModel
    {
        private MusicPageController controller;
        public ObservableCollection<SongViewModel> Songs { get; set; }

        private SongViewModel selectedSong = null;

        private bool isUpdate = false;

        public MusicPageViewModel(ObservableCollection<SongViewModel> songs)
        {
            controller = new MusicPageController();
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

        public bool IsUpdate
        {
            get => isUpdate;
            set
            {
                isUpdate = value;
                OnPropertyChanged("IsUpdate");
            }
        }

        private Command deleteSong;
        private Command updateSong;

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
                        controller.DeleteSong(song.Id);
                        Songs.Remove(song);
                        controller.DeleteFromLocalFileRep(song.song);
                        IndexCorrectAfterDelete();
                    }
                }));
            }
        }

        public Command UpdateSong
        {
            get
            {
                return updateSong ?? (updateSong  =new Command((obj) =>
                {
                    if (SelectedSong != null)
                    {
                        IsUpdate = IsUpdate != true;
                    }
                }));
            }
        }

        public void IndexCorrectAfterDelete()
        {
            int newIdx = 0;
            for (int i = 0;i<Songs.Count;++i)
            {
                var song = Songs[i];
                song.Index = ++newIdx;
            }
        }
    }
}
