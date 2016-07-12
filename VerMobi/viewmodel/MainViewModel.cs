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
            AnzVertr = Zaehler.AnzVertr();
            AnzTelNr = Zaehler.AnzTelNr();
            AnzSimKarten = Zaehler.AnzSimKarten();
            AnzGeraete = Zaehler.AnzGeraete();
            AnzNutzer = Zaehler.AnzNutzer();
            AnzFahrz = Zaehler.AnzFahrz();
            AnzVertrAktiv = Zaehler.AnzVertrAktiv();
            AnzTelNrAktiv = Zaehler.AnzTelNrAktiv();
            AnzSimKartenAktiv = Zaehler.AnzSimKartenAktiv();
        }

        public int AnzSimKartenAktiv { get; set; }

        public int AnzTelNrAktiv { get; set; }

        public int AnzVertrAktiv { get; set; }

        public int AnzFahrz { get; set; }

        public int AnzNutzer { get; set; }

        public int AnzGeraete { get; set; }

        public int AnzSimKarten { get; set; }

        public int AnzTelNr { get; set; }

        public int AnzVertr { get; set; }

        public ICommand ClickCommand { get; set; }

    }
}
