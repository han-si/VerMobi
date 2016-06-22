using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;

namespace VerMobi.viewmodel
{
    [ImplementPropertyChanged]
    class MainViewModel
    {

        public ICommand ClickCommand { get; set; }

    }
}
