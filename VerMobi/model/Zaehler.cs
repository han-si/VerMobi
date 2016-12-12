using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerMobi.model
{
    class Zaehler
    {

        public static int AnzVertr()
        {
            using (var db = new VerMobiEntities())
            {
                var vertraege = db.Vertraege;
                return vertraege.Count();
            }
        }

        public static int AnzVertrAktiv()
        {
            using (var db = new VerMobiEntities())
            {
                var vertraege = db.Vertraege;
                var v = vertraege.Count();
                var heute = DateTime.Today;
                var inaktiv = db.Vertraege.Where(e => e.vertragsende <= heute);
                var i = inaktiv.Count();
                return v - i;
            }
        }

        public static int AnzTelNr()
        {
            using (var db = new VerMobiEntities())
            {
                var nummern = db.Telefonnummern;
                return nummern.Count();
            }
        }

        public static int AnzTelNrAktiv()
        {
            using (var db = new VerMobiEntities())
            {
                var nummern = db.Telefonnummern;
                var vertraege = db.Vertraege;
                var n = nummern.Count();
                var heute = DateTime.Today;

                var query = from t in nummern
                    join v in vertraege on t.vertragID equals v.vertragID
                    select new {ende = v.vertragsende};
                var inaktiv = query.Where(e => e.ende <= heute);
                var i = inaktiv.Count();
                return n - i;
            }
        }

        public static int AnzSimKarten()
        {
            using (var db = new VerMobiEntities())
            {
                var karten = db.Simkarten;
                return karten.Count();
            }
        }

        public static int AnzSimKartenAktiv()
        {
            using (var db = new VerMobiEntities())
            {
                var karten = db.Simkarten;
                var nummern = db.Telefonnummern;
                var vertraege = db.Vertraege;
                var n = karten.Count();
                var heute = DateTime.Today;

                var query = from k in karten
                            join t in nummern on k.telnrID equals t.telnrID 
                            join v in vertraege on t.vertragID equals v.vertragID
                            select new { ende = v.vertragsende };
                var inaktiv = query.Where(e => e.ende <= heute);
                var i = inaktiv.Count();
                return n - i;
            }
        }

        public static int AnzGeraete()
        {
            using (var db = new VerMobiEntities())
            {
                var geraete = db.Geraete;
                return geraete.Count();
            }
        }

        public static int AnzNutzer()
        {
            using (var db = new VerMobiEntities())
            {
                var nutzer = db.Nutzer;
                return nutzer.Count();
            }
        }
        public static int AnzFahrz()
        {
            using (var db = new VerMobiEntities())
            {
                var fahrzeuge = db.Fahrzeuge;
                return fahrzeuge.Count();
            }
        }
         
    }
}
