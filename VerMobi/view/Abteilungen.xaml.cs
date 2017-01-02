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
using MessageBox = System.Windows.MessageBox;
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
            InitComboBox();
        }

        private void InitComboBox()
        {
            using (var db = new VerMobiEntities())
            {
                var treffer = from firm in db.Firmen
                    select new TempTabelle
                    {
                        Firma = firm.firmaname1
                    };
                foreach (var e in treffer)
                {
                    AbtComboBoxAbtFirma.Items.Add(e.Firma);
                }
            }
        }

        private void AbtButtonFormAbtZuruecksetzen_Click(object sender, RoutedEventArgs e)
        {
            AbtLabelAbtIdText.Content = string.Empty;
            AbtTextBoxAbtBeschr.Clear();
            AbtTextBoxAbtName.Clear();
            AbtTextBoxAbtSuche.Clear();
            AbtComboBoxAbtFirma.SelectedIndex = -1;
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
                        Id = abt.abteilungID,
                        Abteilung = abt.abteilung,
                        Firma = abt.Firmen.firmaname1,
                        Notiz = abt.abteilungbeschr
                    };
                foreach (var info in treffer)
                {
                    Console.WriteLine(@"{0} | {1} | {2} | {3}", info.Id, info.Abteilung, info.Firma, info.Notiz);
                }

                AbtDataGridAbtListe.ItemsSource = treffer.ToList();

            }
        }

        private void AbtDataGridAbtListe_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectRow = AbtDataGridAbtListe.SelectedItem;
            if (selectRow == null) return;

            using (var db = new VerMobiEntities())
            {
                var abtid = ((VerMobi.model.TempTabelle)(AbtDataGridAbtListe.SelectedItem)).Id;
                AbtLabelAbtIdText.Content = abtid;
                var abtn = ((VerMobi.model.TempTabelle)(AbtDataGridAbtListe.SelectedItem)).Abteilung;
                AbtTextBoxAbtName.Text = abtn;
                var abtb = ((VerMobi.model.TempTabelle)(AbtDataGridAbtListe.SelectedItem)).Notiz;
                AbtTextBoxAbtBeschr.Text = abtb;
                var abtf = ((VerMobi.model.TempTabelle)(AbtDataGridAbtListe.SelectedItem)).Firma;
                AbtComboBoxAbtFirma.SelectedItem = abtf;

                //Ausgabe zum Test ob die richtige Abteilungs-ID ankommt
                Console.WriteLine(@"======================================");
                Console.WriteLine(@"ID der selektierten Abteilung ist : {0} ", abtid);
                Console.WriteLine(@"======================================");

                var treffer =
                    from abt in db.Abteilungen
                    join nutz in db.Nutzer on abt.abteilungID equals nutz.abteilungID

                    join simn in db.Simnutzung on nutz.nutzerID equals simn.nutzerID
                    join simk in db.Simkarten on simn.simkartenID equals simk.simkartenID
                
                    where abt.abteilungID == abtid
                    && simn.simrueckgabe == null
                
                    select new TempTabelle
                    {
                        Id = abt.abteilungID,
                        Abteilung = abt.abteilung,
                        Firma = abt.Firmen.firmaname1,
                        Vertragsnr = simn.Simkarten.Telefonnummern.Vertraege.vertragsnr,
                        Telnr = simn.Simkarten.Telefonnummern.telnr,
                        Simkarte = simk.simkartennr,
                        Vorname = nutz.vorname,
                        Nachname =nutz.nachname,
                        Fahrzeug = simn.Fahrzeuge.fahrzeugkennzeichen,
                        Geraet = simn.Geraete.geraetenr,
                        Notiz = abt.abteilungbeschr
                    };
                foreach (var info in treffer)
                {
                    Console.WriteLine(@"{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10}", 
                        info.Id, info.Abteilung, info.Firma, info.Vertragsnr, info.Telnr, info.Simkarte, info.Vorname, info.Nachname, info.Fahrzeug, info.Geraet, info.Notiz);
                }

                AbtDataGridAbtVertrTel.ItemsSource = treffer.ToList();

            }
        }
    }
}
