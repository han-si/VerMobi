//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VerMobi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Geraete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Geraete()
        {
            this.Simnutzung = new HashSet<Simnutzung>();
        }
    
        public int geraeteID { get; set; }
        public string geraetenr { get; set; }
        public string geraeteinventarnr { get; set; }
        public string geraeteimei { get; set; }
        public int geraetetypID { get; set; }
        public Nullable<int> geraeteherstID { get; set; }
        public Nullable<int> geraetemodellID { get; set; }
        public Nullable<int> simgroesseID { get; set; }
        public Nullable<int> betriebssysID { get; set; }
        public Nullable<System.DateTime> inbetriebnahme { get; set; }
        public string geraetenotiz { get; set; }
    
        public virtual Betriebssysteme Betriebssysteme { get; set; }
        public virtual Geraetehersteller Geraetehersteller { get; set; }
        public virtual Geraetemodelle Geraetemodelle { get; set; }
        public virtual Geraetetypen Geraetetypen { get; set; }
        public virtual Simgroessen Simgroessen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Simnutzung> Simnutzung { get; set; }
    }
}
