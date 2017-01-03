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
using System.Windows.Forms.VisualStyles;
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
            AbtLabelAbtIdText.Content = null;
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

        private void AbtButtonAbtNeuAnlegen_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new VerMobiEntities())
            {
                if (AbtLabelAbtIdText.Content != null)
                {
                    MessageBox.Show(
                        "Damit eine neue Abteilung angelegt werden kann, darf keine Abteilungs-ID angegeben werden. " +
                        "Bitte einmal die 'Textfelder leeren'.", "Info",
                        MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    AbtButtonFormAbtZuruecksetzen.Focus();
                    return;
                }

                if (AbtTextBoxAbtName.Text == "")
                {
                    MessageBox.Show(
                        "Das Feld 'Abteilung' darf nicht leer sein. " +
                        "\r\n Bitte eine Bezeichnung für die Abteilung angeben.", "Info",
                        MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    AbtTextBoxAbtName.Focus(); 
                    return;
                }

                if (AbtTextBoxAbtName.Text != "")
                {
                    var abtn = AbtTextBoxAbtName.Text;
                    var abtf = AbtComboBoxAbtFirma.Text;
                    var abtb = AbtTextBoxAbtBeschr.Text;

                    if (AbtComboBoxAbtFirma.Text == "")
                    {
                        MessageBox.Show(
                            "Bitte die zugehörige Firma auswählen. " +
                            "\r\n \r\n Wenn diese noch nicht auswählbar ist, dann legen Sie die Firme bitte zuerst im" +
                            " entsprechenden Formular neu an.", "Info",
                            MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        AbtComboBoxAbtFirma.Focus();
                        return;
                    }

                    var treffer = from abt in db.Abteilungen
                        where abt.abteilung == abtn
                        select new TempTabelle
                        {
                            Firma = abt.Firmen.firmaname1
                        };
                    var firstOrDefault = treffer.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        if (abtf == firstOrDefault.Firma)
                        {
                            MessageBox.Show(string.Format(
                                "Die Datenbank enthält schon eine Abteilung '{0}' in der Firma '{1}'. Bitte wählen Sie eine andere Firma aus. " +
                                "\r\n \r\n Wenn Sie stattdessen eine bestehende Abteilung bearbeiten möchten, dann bitte die Abteilung zuerst " +
                                "suchen und in der oberen Tabelle auswählen.", abtn, abtf), "Info",
                                MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                            AbtComboBoxAbtFirma.Focus();
                            return;
                        }
                    }

                    //ID der ausgewählten Firma ermitteln
                    var f = from firm in db.Firmen
                        where firm.firmaname1 == abtf
                        select new TempTabelle
                        {
                            Id = firm.firmaID
                        };
                    var first = f.FirstOrDefault();
                    if (first != null)
                    {
                        var firmid = first.Id;
                        //Abteilungen newAbteilungen = model.Abteilungen.CreateAbteilungen(abtn, firmid, abtb);
                        Console.WriteLine(@"{0} | {1} | {2} ", abtn, firmid, abtb);
                    }
                }
            }
        }
    }
}
