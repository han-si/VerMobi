using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using VerMobi.model;

namespace VerMobi.viewmodel
{
    [ImplementPropertyChanged]
    class MainViewModel
    {
        public MainViewModel()
        {
            AnzVertr = FooBaa.AnzVertr();
        }


        public ICommand ClickCommand { get; set; }

        public int AnzVertr { get; set; }

    }
}
