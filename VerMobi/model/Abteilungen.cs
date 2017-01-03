using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerMobi.model
{
    public partial class Abteilungen
    {
        private string Abteilung;
        private int FirmaID;
        private string AbteilungBeschr;

        public static Abteilungen CreateAbteilungen(global::System.String abteilung,
                                                    global::System.Int32 firmaID,
                                                    global::System.String abteilungbeschr)
        {
            Abteilungen abteilungen = new Abteilungen();
            abteilungen.Abteilung = abteilung;
            abteilungen.FirmaID = firmaID;
            abteilungen.AbteilungBeschr = abteilungbeschr;
            return abteilungen;
        }
    }
}
