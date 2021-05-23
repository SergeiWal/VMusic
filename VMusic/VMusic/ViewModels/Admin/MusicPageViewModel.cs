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
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Admin
{
    class MusicPageViewModel: BaseViewModel
    {

        private const string FILE_NAME_DELIMITOR = "_";
        private const string FILE_EXTENSION = ".mp3";
        private const string FILE_PATH_PREFIX = @"..\..\Songs\";

        private UnitOfWork dbWorker;
        public ObservableCollection<SongViewModel> Songs { get; set; }

        private SongViewModel selectedSong = null;

        private bool isUpdate = false;

        public MusicPageViewModel(ObservableCollection<SongViewModel> songs)
        {
            dbWorker = new UnitOfWork();
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
                        dbWorker.Songs.Delete(song.Id);
                        dbWorker.Save();
                        Songs.Remove(song);
                        DeleteFromLocalFileRep(song);
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

        private void DeleteFromLocalFileRep(SongViewModel song)
        {
            FileInfo fileInfo = new FileInfo(song.Source);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
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
