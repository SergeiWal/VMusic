using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VMusic.Views.Autorization;

namespace VMusic.Commands
{
    class AutorizationCommand
    {
        static AutorizationCommand() 
        {
            Exit = new RoutedCommand("Exit", typeof(Login));
        }

        public static ICommand Exit { get;  set; }
    }
}
