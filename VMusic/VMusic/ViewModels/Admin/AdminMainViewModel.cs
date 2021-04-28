using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VMusic.Commands;
using VMusic.Repository;
using VMusic.Views.Admin;

namespace VMusic.ViewModels.Admin
{
    class AdminMainViewModel: BaseWindowViewModel
    {

        private UserPage userPage;
        private AddMusicPage addMusicPage;
        private MusicPage musicPage;
        private TopMusicPage topMusicList;
        private Page currentPage;

        public AdminMainViewModel(Window owner) : base(owner)
        {
            userPage = new UserPage();
            addMusicPage = new AddMusicPage();
            musicPage = new MusicPage();
            topMusicList = new TopMusicPage();

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
