using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VMusic.Views.Admin;

namespace VMusic.ViewModels.Admin
{
    class AdminMainViewModel: BaseWindowViewModel
    {

        private MusicPage musicPage;
        private Page currentPage;

        public AdminMainViewModel(Window owner) : base(owner)
        {
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
    }
}
