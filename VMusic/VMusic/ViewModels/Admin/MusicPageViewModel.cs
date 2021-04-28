using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Admin
{
    class MusicPageViewModel: BaseViewModel
    {
        private SongRepository repository;
        public ObservableCollection<SongViewModel> Songs { get; set; }

        public MusicPageViewModel()
        {
            repository = new SongRepository();
            Songs = new ObservableCollection<SongViewModel>(repository.GetAllObject().Select(s=> new SongViewModel(s)));
        }
    }
}
