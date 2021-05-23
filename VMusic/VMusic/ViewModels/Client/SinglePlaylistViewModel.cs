using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Commands;
using VMusic.Controller.Client.Player;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class SinglePlaylistViewModel: BaseViewModel
    {
        private User user;
        private Player player;
        private UnitOfWork dbWorker;
        private SongViewModel selectedSong;
        private bool isUpdate = false;
        private int songItemCount = 0;

        public PlaylistViewModel Playlist { get; set; }
        public ObservableCollection<SongViewModel> SongLocalList { get; set; }

        public SinglePlaylistViewModel(PlaylistViewModel playlist, Player player, User user)
        {
            dbWorker = new UnitOfWork();
            this.user = user;
            this.player = player;
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
                player.CurrentPlaylist = SongLocalList;
                player.CurrentSong = value;
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

        private Command play;
        private Command update;

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

        public Command Update
        {
            get
            {
                return update ?? (update = new Command((obj) =>
                {
                    IsUpdate = IsUpdate != true;
                }));
            }
        }

        private void CollectionInitialized()
        {
            var obj = dbWorker.Playlist.GetByPredicate(p => p.Id == Playlist.Id);
            if (obj != null)
            {
                SongLocalList = new ObservableCollection<SongViewModel>(obj.Songs.Select(s => new SongViewModel(s)
                    {Index = ++songItemCount}));
            }
        }

    }
}
