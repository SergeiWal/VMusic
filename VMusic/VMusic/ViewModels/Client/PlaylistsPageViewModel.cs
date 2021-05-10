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
    class PlaylistsPageViewModel: BaseViewModel
    {
        private UnitOfWork dbWorker;
        private PlaylistViewModel selectedPlaylist;
        private User currentUser;
        public ObservableCollection<PlaylistViewModel> Playlists { get; set; }


        public PlaylistsPageViewModel(User user)
        {
            dbWorker = new UnitOfWork();
            currentUser = user;
            Playlists = new ObservableCollection<PlaylistViewModel>(dbWorker.Playlist.GetAllObject()
                .Where(p=>p.UserId == user.Id)
                .Select(p=>new PlaylistViewModel(p)));
        }

        public PlaylistViewModel SelectedPlaylist
        {
            get => selectedPlaylist;
            set
            {
                selectedPlaylist = value;
                OnPropertyChanged("SelectedPlaylist");
            }
        }

        public void PlaylistsDataUpdate(Playlist playlist)
        {
            Playlists.Add(new PlaylistViewModel(playlist));
        }
    }
}
