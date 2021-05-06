using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VMusic.Commands;
using VMusic.Models;
using VMusic.Repository;
using VMusic.Views.Admin;

namespace VMusic.ViewModels.Admin
{
    class AdminMainViewModel: BaseWindowViewModel
    {
        private User admin;
        private UnitOfWork dbWorker;
        public ObservableCollection<SongViewModel> LocalSongList { get; set; }
        public ObservableCollection<UserViewModel> UserLocalCollection { get; set; }

        private UserPage userPage;
        private AddMusicPage addMusicPage;
        private MusicPage musicPage;
        private TopMusicPage topMusicList;
        private Page currentPage; 

        public AdminMainViewModel(User admin, Window owner) : base(owner)
        {
            this.admin = admin;
            dbWorker = new UnitOfWork();


            LocalSongList = new ObservableCollection<SongViewModel>(dbWorker.Songs.GetAllObject()
                .Select(b => new SongViewModel(b)));
            UserLocalCollection =
                new ObservableCollection<UserViewModel>(dbWorker.Users.GetAllObject().Where(u=>!u.IsAdmin)
                    .Select(u => new UserViewModel(u)));

            userPage = new UserPage();
            userPage.DataContext = new UserPageViewModel(admin, UserLocalCollection);

            addMusicPage = new AddMusicPage();
            addMusicPage.DataContext = new AddMusicViewModel(LocalSongList);
            
            musicPage = new MusicPage();
            musicPage.DataContext = new MusicPageViewModel(LocalSongList);

            topMusicList = new TopMusicPage();
            topMusicList.DataContext = new TopMusicPageViewModel();

            CurrentPage = musicPage;
        }

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private Command switchToAddMusic;
        private Command switchToMusicList;
        private Command switchToUserList;
        private Command switchToTopMusicList;

        public Command SwitchToMusicList
        {
            get
            {
                return switchToMusicList ?? (switchToMusicList = new Command((obj) =>
                {
                    CurrentPage = musicPage;
                }));
            }
        }

        public Command SwitchToTopMusicList
        {
            get
            {
                return switchToTopMusicList ?? (switchToTopMusicList = new Command((obj) =>
                {
                    CurrentPage = topMusicList;
                }));
            }
        }


        public Command SwitchToAddMusic
        {
            get
            {
                return switchToAddMusic ?? (switchToAddMusic = new Command((obj) =>
                {
                    CurrentPage = addMusicPage;
                }));
            }
        }

        public Command SwitchToUserList
        {
            get
            {
                return switchToUserList ?? (switchToUserList = new Command((obj) =>
                {
                    CurrentPage = userPage;
                }));
            }
        }

    }
}
