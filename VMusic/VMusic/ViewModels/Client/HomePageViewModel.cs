using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class HomePageViewModel: BaseViewModel
    {

        private SongRepository songRepository;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }

        public HomePageViewModel()
        {
            songRepository = new SongRepository();
            LocalSongList =
                new ObservableCollection<SongViewModel>(songRepository.GetAllObject()
                    .Select(o => new SongViewModel(o)));
        }

    }
}
