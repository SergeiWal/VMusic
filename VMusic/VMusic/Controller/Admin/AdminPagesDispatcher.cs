using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using VMusic.Models;
using VMusic.ViewModels;
using VMusic.ViewModels.Admin;
using VMusic.Views.Admin;

namespace VMusic.Controller.Admin
{
    class AdminPagesDispatcher
    {
        public UserPage UserPage { get; set; }
       public AddMusicPage AddMusicPage { get; set; }
       public MusicPage MusicPage { get; set; }
       public TopMusicPage TopMusicList { get; set; }
       public UpdateMusicPage UpdateMusicPage { get; set; }
       public Page CurrentPage { get; set; }

       public AdminPagesDispatcher(User admin, ObservableCollection<UserViewModel> UserLocalCollection,
           ObservableCollection<SongViewModel> LocalSongList, PropertyChangedEventHandler callback)
       {
           UserPage = new UserPage();
           UserPage.DataContext = new UserPageViewModel(admin, UserLocalCollection);

           AddMusicPage = new AddMusicPage();
           AddMusicPage.DataContext = new AddMusicViewModel(LocalSongList);

           MusicPage = new MusicPage();
           MusicPageViewModel musicPageViewModel = new MusicPageViewModel(LocalSongList);
           musicPageViewModel.PropertyChanged += callback;
           MusicPage.DataContext = musicPageViewModel;

           TopMusicList = new TopMusicPage();
           TopMusicList.DataContext = new TopMusicPageViewModel();

           UpdateMusicPage = new UpdateMusicPage();
           CurrentPage = MusicPage;
       }

    }
}
