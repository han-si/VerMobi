using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerMobi.model
{
    class FooBaa
    {

        public static int AnzVertr()
        {
            using (var db = new VerMobiEntities())
            {
                var vertraege = db.vertraege;
                return vertraege.Count();
            }
        }
 
    }
}
