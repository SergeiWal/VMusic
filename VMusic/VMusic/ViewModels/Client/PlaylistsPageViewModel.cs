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
        private PlaylistViewModel selectedPlaylist;
        public ObservableCollection<PlaylistViewModel> Playlists { get; set; }


        public PlaylistsPageViewModel(User user)
        {
            Playlists = new ObservableCollection<PlaylistViewModel>(GetPlaylist()
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

        private IEnumerable<Playlist> GetPlaylist()
        {
            return (new UnitOfWork()).Playlist.GetAllObject();
        }

        public void PlaylistsDataUpdate(Playlist playlist)
        {
            Playlists.Add(new PlaylistViewModel(playlist));
        }
    }
}
