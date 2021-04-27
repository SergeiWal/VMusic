using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VMusic.Commands;
using VMusic.Views.Admin;

namespace VMusic.ViewModels.Admin
{
    class AdminMainViewModel: BaseWindowViewModel
    {
        private AddMusicPage addMusicPage;
        private MusicPage musicPage;
        private Page currentPage;

        public AdminMainViewModel(Window owner) : base(owner)
        {
            addMusicPage = new AddMusicPage();
            musicPage = new MusicPage();

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

    }
}
