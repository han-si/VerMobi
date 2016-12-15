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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VerMobi.model;
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
                foreach (var info in treffer)
                {
                    Console.WriteLine(@"{0} | {1} | {2} | {3}", info.ID, info.Abteilung, info.Firma, info.Notiz);
                }

                AbtDataGridAbtListe.ItemsSource = treffer.ToList();

                #region

                //// Bind the System.Windows.Forms.DataGridView object
                //// to the System.Windows.Forms.BindingSource object.
                //DataGridView.DataSource = AbtDataGridAbtListe;

                //// Fill the DataSet.
                //DataSet ds = new DataSet { Locale = CultureInfo.InvariantCulture };
                //FillDataSet(ds);

                //DataTable dt = ds.Tables["Abteilungen"];

                //IEnumerable<DataRow> query =
                //    from abt in dt.AsEnumerable()
                //    where abt.Field<string>("abteilung") == AbtTextBoxAbtSuche.Text
                //    select abt;

                //DataTable boundTable = DataTableExtensions.CopyToDataTable(query);

                //AbtDataGridAbtListe.DataSource = boundTable;

                #endregion

            }
        }
    }
}
