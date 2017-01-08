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

        private void AbtNameLeer()
        {
            MessageBox.Show(
                "Das Feld 'Abteilung' darf nicht leer sein." +
                "\r\n \r\nBitte eine Bezeichnung oder Namen für die Abteilung angeben.", "Info",
                MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            AbtTextBoxAbtName.Focus();
        }

        private void AbtButtonFormAbtZuruecksetzen_Click(object sender, RoutedEventArgs e)
        {
            AbtTextBoxAbtSuche.Clear();
            AbtLabelAbtSucheKeinErgebnis.Content = null;
            AbtLabelAbtIdText.Content = null;
            AbtTextBoxAbtName.Clear();
            AbtComboBoxAbtFirma.SelectedIndex = -1;
            AbtTextBoxAbtBeschr.Clear();
            AbtLabelAbtGesp.Content = null;
        }

        private void AbtButtonAbtSuchen_Click(object sender, RoutedEventArgs e)
        {
            AbtSuchen();
        }

        private void AbtSuchen()
        {
            AbtLabelAbtSucheKeinErgebnis.Content = null;
            AbtLabelAbtGesp.Content = null;
            using (var db = new VerMobiEntities())
            {
                var treffer =
                    from abt in db.Abteilungen
                    where abt.abteilung.Contains(AbtTextBoxAbtSuche.Text)
                    orderby abt.abteilung
                    select new TempTabelle
                    {
                        Id = abt.abteilungID,
                        Abteilung = abt.abteilung,
                        Firma = abt.Firmen.firmaname1,
                        Notiz = abt.abteilungbeschr
                    };

                if (!treffer.Any())
                {
                    AbtLabelAbtSucheKeinErgebnis.Content = "Kein Treffer! Alternativ können Sie das Feld leer lassen um alle Abt. anzuzeigen.";
                }

                foreach (var info in treffer)
                {
                    Console.WriteLine(@"{0} | {1} | {2} | {3}", info.Id, info.Abteilung, info.Firma, info.Notiz);
                }
                AbtDataGridAbtListe.ItemsSource = treffer.ToList();
                AbtTextBoxAbtSuche.Focus();
            }
        }

        private void AbtDataGridAbtListe_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AbtLabelAbtGesp.Content = null;
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
            AbtLabelAbtGesp.Content = null;
            using (var db = new VerMobiEntities())
            {
                //prüfen ob schon eine Abteilungs-ID angezeigt wird, 
                //um zu verhindern das unbeabsichtigt eine schon bestehende Abt. geändert wird
                if (AbtLabelAbtIdText.Content != null)
                {
                    MessageBox.Show(
                        "Damit eine neue Abteilung angelegt werden kann, bitte vorher einmal die 'Textfelder leeren'.", "Info",
                        MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    AbtButtonFormAbtZuruecksetzen.Focus();
                    return;
                }

                if (AbtTextBoxAbtName.Text == "")
                {
                    AbtNameLeer(); 
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
                            "Bitte die zugehörige Firma auswählen." +
                            "\r\n \r\nWenn diese noch nicht auswählbar ist, dann legen Sie die Firme bitte zuerst im" +
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

                    var stringListe = new List<string>();
                    foreach (var info in treffer)
                    {
                        stringListe.Add(info.Firma);
                    }

                    if (stringListe.Contains(abtf))
                    {
                        foreach (var el in stringListe)
                        {
                            Console.WriteLine(el);
                        }

                        MessageBox.Show(string.Format(
                            "Die Datenbank enthält schon eine Abteilung '{0}' in der Firma '{1}'. Bitte  wählen Sie eine andere Firma aus. " +
                            "\r\n \r\n Wenn Sie stattdessen eine bestehende Abteilung bearbeiten möchten, dann bitte die Abteilung zuerst " +
                            "suchen und in der oberen Tabelle auswählen.", abtn, abtf), "Info",
                            MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                        AbtComboBoxAbtFirma.Focus();
                        return;
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
                        
                        //eine neue Abteilung erstellen und an die DB übergeben
                        VerMobi.Abteilungen abteilungen = new VerMobi.Abteilungen
                        {
                            abteilung = abtn,
                            firmaID = firmid,
                            abteilungbeschr = abtb
                        };
                        db.Abteilungen.Add(abteilungen);
                        db.SaveChanges();

                        //ermitteln und ausgeben mit welcher ID die letzte Abteilung gespeichert wurde
                        //damit der Benutzer eine Rückmeldung hat ob das Speichern erfolgreich war
                        var ida = from abt in db.Abteilungen 
                                      where abt.abteilung == abtn
                                      select new TempTabelle
                                      {
                                          Id = abt.abteilungID
                                      };
                        var abtid = (ida.ToList()).Last();

                        AbtSuchen(); //damit sich das obere DataGrid aktualisiert
                        AbtLabelAbtGesp.Content = "INFO:  Es wurde eine neue Abteilung  '" + abtn + "'  mit der ID  '" + abtid.Id + "'  angelegt.";
                        Console.WriteLine(@"{0} | {1} | {2} ", abtn, abtid.Id, abtb);
                        AbtTextBoxAbtName.Focus();
                    }
                }
            }
        }

        private void AbtButtonAbtAendernSpeich_Click(object sender, RoutedEventArgs e)
        {
            AbtLabelAbtGesp.Content = null;
            using (var db = new VerMobiEntities())
            {
                //prüfen ob und welche Abteilungs-ID angezeigt wird um zu verhindern das eine neue Abteilung angelegt wird
                if (AbtLabelAbtIdText.Content == null)
                {
                    MessageBox.Show(
                        "Damit eine bestehenden Abteilung geändert werden kann, bitte zuerst die entsprechende Abteilung " +
                        "in der oberen Liste auswählen und dann die gewünschten Änderungen vornehmen.", "Info",
                        MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                    AbtTextBoxAbtSuche.Focus();
                    return;
                }

                if (AbtTextBoxAbtName.Text == "")
                {
                    AbtNameLeer();
                    return;
                }

                if (AbtTextBoxAbtName.Text != "")
                {
                    var abtid = (int) AbtLabelAbtIdText.Content;
                    var abtn = AbtTextBoxAbtName.Text;
                    var abtf = AbtComboBoxAbtFirma.Text;
                    var abtb = AbtTextBoxAbtBeschr.Text;

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

                        //die zu ändernde Abteilung aus der DB abrufen, ändern und wieder an die DB übergeben
                        var treffer = from abt in db.Abteilungen
                            where abt.abteilungID == abtid
                            select abt;

                        foreach (var abt in treffer)
                        {
                            abt.abteilung = abtn;
                            abt.firmaID = firmid;
                            abt.abteilungbeschr = abtb;
                        }
                        db.SaveChanges();

                        AbtSuchen(); //damit sich das obere DataGrid aktualisiert
                        Console.WriteLine(@"{0} | {1} | {2} | {3} ", abtid, abtn, abtf, abtb);
                        AbtLabelAbtGesp.Content = "INFO:  Die Abteilung  '" + abtn + "'  mit der ID  '" + abtid + "'  wurde geändert.";
                    }
                }
            }
        }
    }
}
