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
        public ObservableCollection<Song> Songs;

        public MusicPageViewModel(ICollection<Song> songs)
        {
            Songs = new ObservableCollection<Song>();
        }
    }
}
