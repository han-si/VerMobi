using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerMobi.model
{
    //Quelle: https://msdn.microsoft.com/de-de/library/bb386921(v=vs.110).aspx
    public static class CustomLinQtoDataSetMethods
    {
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }
    }
}
