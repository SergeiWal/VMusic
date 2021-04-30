using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VMusic.Commands;
using VMusic.Views.Client;

namespace VMusic.ViewModels.Client
{
    class ClientMainViewModel: BaseWindowViewModel
    {
        private HomePage homePage;
        private Page currentPage;

        public ClientMainViewModel(Window owner) : base(owner)
        {
            homePage = new HomePage();
            homePage.DataContext = new HomePageViewModel();

            CurrentPage = homePage;
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


        private Command switchToHomePage;

        public Command SwitchToHomePage
        {
            get
            {
                return switchToHomePage??(switchToHomePage = new Command((obj) =>
                {
                    CurrentPage = homePage;
                }));
            }
        }
    }
}
