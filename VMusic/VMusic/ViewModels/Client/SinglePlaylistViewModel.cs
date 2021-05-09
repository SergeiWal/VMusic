using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Commands;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class SinglePlaylistViewModel: BaseViewModel
    {
        private User user;
        private SongContent songContent;
        private UnitOfWork dbWorker;
        private SongViewModel selectedSong;

        public PlaylistViewModel Playlist { get; set; }
        public ObservableCollection<SongViewModel> SongLocalList { get; set; }

        public SinglePlaylistViewModel(PlaylistViewModel playlist, SongContent songContent, User user)
        {
            dbWorker = new UnitOfWork();
            this.user = user;
            this.songContent = songContent;
            Playlist = playlist;
            CollectionInitialized();
        }

        public string Name
        {
            get => Playlist.Name;
            set
            {
                Playlist.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Owner
        {
            get => user.Name;
            set
            {
                user.Name = value;
                OnPropertyChanged("Owner");
            }
        }

        public SongViewModel SelectedSong
        {
            get => selectedSong;
            set
            {
                selectedSong = value;
                songContent.CurrentPlaylist = SongLocalList;
                songContent.CurrentSong = value;
                OnPropertyChanged("SelectedSong");
            }
        }

        private Command play;

        public Command Play
        {
            get
            {
                return play ??(play = new Command((obj) =>
                {
                    if (SongLocalList!=null && SongLocalList.Count!=0)
                    {
                        SelectedSong = SongLocalList[0];
                    }
                }));
            }
        }

        private void CollectionInitialized()
        {
            var obj = dbWorker.Playlist.GetByPredicate(p => p.Id == Playlist.Id);
            SongLocalList = new ObservableCollection<SongViewModel>(obj.Songs.Select(s => new SongViewModel(s)));
        }

    }
}
