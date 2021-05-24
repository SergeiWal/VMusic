using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using VMusic.Commands;
using VMusic.Controller.Admin;
using VMusic.Models;

namespace VMusic.ViewModels.Admin
{
    class AdminMainViewModel: BaseWindowViewModel
    {
        private AdminMainController controller;
        private int songItemCount = 0;
        private int userItemCount = 0;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }
        public ObservableCollection<UserViewModel> UserLocalCollection { get; set; }

        private AdminPagesDispatcher pagesDispatcher;

        public AdminMainViewModel(User admin)
        {
            controller = new AdminMainController();

            LocalSongList = new ObservableCollection<SongViewModel>(controller.GetSongs()
                .Select(b => new SongViewModel(b){Index = ++songItemCount}));
            UserLocalCollection =
                new ObservableCollection<UserViewModel>(controller.GetUser()
                    .Select(u => new UserViewModel(u){Index = ++userItemCount}));

            pagesDispatcher =
                new AdminPagesDispatcher(admin, UserLocalCollection, LocalSongList, OnSongUpdatePropertyChanged);
        }

        public Page CurrentPage
        {
            get => pagesDispatcher.CurrentPage;
            set
            {
                pagesDispatcher.CurrentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private Command switchToAddMusic;
        private Command switchToMusicList;
        private Command switchToUserList;
        private Command switchToTopMusicList;
        private Command switchToUpdateMusic;

        public Command SwitchToMusicList
        {
            get
            {
                return switchToMusicList ?? (switchToMusicList = new Command((obj) =>
                {
                    CurrentPage = pagesDispatcher.MusicPage;
                }));
            }
        }

        public Command SwitchToTopMusicList
        {
            get
            {
                return switchToTopMusicList ?? (switchToTopMusicList = new Command((obj) =>
                {
                    CurrentPage = pagesDispatcher.TopMusicList;
                }));
            }
        }


        public Command SwitchToAddMusic
        {
            get
            {
                return switchToAddMusic ?? (switchToAddMusic = new Command((obj) =>
                {
                    CurrentPage = pagesDispatcher.AddMusicPage;
                }));
            }
        }

        public Command SwitchToUserList
        {
            get
            {
                return switchToUserList ?? (switchToUserList = new Command((obj) =>
                {
                    CurrentPage = pagesDispatcher.UserPage;
                }));
            }
        }

        public Command SwitchToUpdateMusic
        {
            get
            {
                return switchToUpdateMusic ?? (switchToUpdateMusic = new Command((obj) =>
                {
                    CurrentPage = pagesDispatcher.UpdateMusicPage;
                }));
            }
        }

        private void OnSongUpdatePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsUpdate")
            {
                MusicPageViewModel musicPageViewModel = (MusicPageViewModel)pagesDispatcher.MusicPage.DataContext;
                UpdateMusicViewModel updateMusicViewModel = new UpdateMusicViewModel(musicPageViewModel.SelectedSong, LocalSongList);
                updateMusicViewModel.PropertyChanged += OnSongUpdateFinishPropertyChanged;
                pagesDispatcher.UpdateMusicPage.DataContext = updateMusicViewModel;
                SwitchToUpdateMusic.Execute(new object());
            }
        }

        private void OnSongUpdateFinishPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFinish")
            {
                SwitchToMusicList.Execute(new object());
            }
        }
    }
}
