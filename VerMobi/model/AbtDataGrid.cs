using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerMobi.model
{
    public class AbtDataGrid
    {
        public static Object AbtDataGrid1()
        {
            var db = new VerMobiEntities();
            db.Abteilungen.Load();
            return db.Abteilungen.Local;
        }
    }
}
