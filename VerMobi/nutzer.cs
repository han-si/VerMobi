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
    
    public partial class Nutzer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nutzer()
        {
            this.Simnutzung = new HashSet<Simnutzung>();
        }
    
        public int nutzerID { get; set; }
        public string vorname { get; set; }
        public string nachname { get; set; }
        public int abteilungID { get; set; }
        public string nutzernotiz { get; set; }
    
        public virtual Abteilungen Abteilungen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Simnutzung> Simnutzung { get; set; }
    }
}
