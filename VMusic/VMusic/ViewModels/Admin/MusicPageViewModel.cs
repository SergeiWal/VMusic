using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Models;

namespace VMusic.ViewModels.Admin
{
    class MusicPageViewModel: BaseViewModel
    {
        public ObservableCollection<SongViewModel> Songs;

        public MusicPageViewModel(List<Song> songs)
        {
            Songs = new ObservableCollection<SongViewModel>(songs.Select(s=> new SongViewModel(s)));
        }
    }
}
