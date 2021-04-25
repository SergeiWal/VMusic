using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VMusic.Commands;

namespace VMusic.ViewModels
{
    class BaseWindowViewModel: BaseViewModel
    {

        protected Window owner;

        public BaseWindowViewModel(Window owner)
        {
            this.owner = owner;
        }


        protected void SwitchTo(Window window, Window owner)
        {
            window.Show();
            owner.Close();
        }

        private Command exitCommand;
        private Command windowSizeChangeCommand;
        private Command collapseWindow;

        public Command ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new Command((obj) => {
                    owner.Close();
                }));
            }
        }

        public Command WindowSizeChangeCommand
        {
            get
            {
                return  windowSizeChangeCommand ?? (windowSizeChangeCommand = new Command((obj) =>
                    {
                        owner.WindowState = owner.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
                    }
                ));
            }
        }

        public Command CollapseWindow
        {
            get
            {
                return collapseWindow ?? (collapseWindow = new Command((obj) =>
                    {
                        owner.WindowState = WindowState.Minimized;
                    }
                ));
            }
        }
    }
}
