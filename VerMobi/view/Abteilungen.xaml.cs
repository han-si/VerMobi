using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VerMobi.model;
using DataGrid = System.Windows.Controls.DataGrid;
using UserControl = System.Windows.Controls.UserControl;

namespace VerMobi.view
{
    /// <summary>
    /// Interaktionslogik für Abteilungen.xaml
    /// </summary>
    public partial class Abteilungen : UserControl
    {
        
        public Abteilungen()
        {
            InitializeComponent();

        }

        private void AbtButtonFormAbtZuruecksetzen_Click(object sender, RoutedEventArgs e)
        {
            AbtLabelAbtIdText.Content = string.Empty;
            AbtTextBoxAbtBeschr.Clear();
            AbtTextBoxAbtName.Clear();
            AbtTextBoxAbtSuche.Clear();
        }


        private void AbtButtonAbtSuchen_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new VerMobiEntities())
            {
                var treffer = 
                    from abt in db.Abteilungen
                    where abt.abteilung.StartsWith(AbtTextBoxAbtSuche.Text)
                    select new TempTabelle
                    {
                        ID = abt.abteilungID,
                        Abteilung = abt.abteilung,
                        Firma = abt.Firmen.firmaname1,
                        Notiz = abt.abteilungbeschr
                    };
                //foreach (var info in treffer)
                //{
                //    Console.WriteLine(@"{0} | {1} | {2} | {3}", info.ID, info.Abteilung, info.Firma, info.Notiz);
                //}

                AbtDataGridAbtListe.ItemsSource = treffer.ToList();

            }
        }

        private void AbtDataGridAbtListe_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectRow = AbtDataGridAbtListe.SelectedItem;
            if (selectRow == null) return;

            using (var db = new VerMobiEntities())
            {
                var abtid = ((VerMobi.model.TempTabelle)(AbtDataGridAbtListe.SelectedItem)).ID;
                AbtLabelAbtIdText.Content = abtid;
                var abtt = ((VerMobi.model.TempTabelle)(AbtDataGridAbtListe.SelectedItem)).Abteilung;
                AbtTextBoxAbtName.Text = abtt;
                var abtb = ((VerMobi.model.TempTabelle)(AbtDataGridAbtListe.SelectedItem)).Notiz;
                AbtTextBoxAbtBeschr.Text = abtb;

                Console.WriteLine(@"ID ist : {0} ", abtid);
                Console.WriteLine(@"==================");

                var treffer =
                    from abt in db.Abteilungen
                    join nutz in db.Nutzer on abt.abteilungID equals nutz.abteilungID 

                    //from simn in db.Simnutzung
                    //join nutz in db.Nutzer on simn.nutzerID equals nutz.nutzerID into sn
                    //from nutz in sn
                    //join abt in db.Abteilungen on nutz.abteilungID equals abt.abteilungID
                    //from simk in db. Simkarten
                    //from telnr in db.Telefonnummern
                    //join simn in db.Simnutzung on nutz.nutzerID equals simn.nutzerID
                    //from nutz in db.Nutzer
                    //join nutze in db.Nutzer on simn.nutzerID equals nutze.nutzerID
                    //join simn in db.Simnutzung on nutz.nutzerID equals simn.nutzerID 
                    //join simk in db.Simkarten on simn.simkartenID equals simk.simkartenID
                
                    where abt.abteilungID == abtid
                    //&& nutz.nutzerID == simn.nutzerID
                
                    select new TempTabelle
                    {
                        ID = abt.abteilungID,
                        Abteilung = abt.abteilung,
                        //Vertragsnr = simn.Simkarten.Telefonnummern.Vertraege.vertragsnr,
                        //Telnr = simn.Simkarten.Telefonnummern.telnr,
                        //Simkarte = simk.simkartennr,
                        Vorname = nutz.vorname,
                        Nachname =nutz.nachname,
                        //Fahrzeug = simn.Fahrzeuge.fahrzeugkennzeichen,
                        //Geraet = simn.Geraete.geraetenr,
                        Notiz = abt.abteilungbeschr
                    };
                foreach (var info in treffer)
                {
                    Console.WriteLine(@"{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}", 
                        info.ID, info.Abteilung, info.Vertragsnr, info.Telnr, info.Simkarte, info.Vorname, info.Nachname, info.Fahrzeug, info.Geraet, info.Notiz);
                }

                AbtDataGridAbtVertrTel.ItemsSource = treffer.ToList();

            }
        }
    }
}
