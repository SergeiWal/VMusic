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
    class BaseWindowViewModel: BaseViewModel, IWindowController
    {


        public Action Close { get; set; }
        public Action SizeChange { get; set; }
        public Action Collapse { get; set; }


        protected void SwitchTo(Window window)
        {
            window.Show();
            Close?.Invoke();
        }

        private Command exitCommand;
        private Command windowSizeChangeCommand;
        private Command collapseWindow;

        public Command ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new Command((obj) => {
                    Close?.Invoke();
                }));
            }
        }

        public Command WindowSizeChangeCommand
        {
            get
            {
                return  windowSizeChangeCommand ?? (windowSizeChangeCommand = new Command((obj) =>
                    {
                        SizeChange?.Invoke();
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
                        Collapse?.Invoke();
                    }
                ));
            }
        }
    }
}
