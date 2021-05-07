using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.ViewModels
{
    interface IWindowController
    {
        Action Close { get; set; }
        Action SizeChange { get; set; }
        Action Collapse { get; set; }
    }
}
