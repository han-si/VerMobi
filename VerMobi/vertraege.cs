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
    
    public partial class Vertraege
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vertraege()
        {
            this.Telefonnummern = new HashSet<Telefonnummern>();
        }
    
        public int vertragID { get; set; }
        public string vertragsnr { get; set; }
        public int vertragsartID { get; set; }
        public Nullable<int> verkaeuferID { get; set; }
        public int providerID { get; set; }
        public System.DateTime vertragsbeginn { get; set; }
        public Nullable<System.DateTime> kuendigungsfrist { get; set; }
        public Nullable<System.DateTime> gekuendigtam { get; set; }
        public Nullable<System.DateTime> gekuendigtzum { get; set; }
        public Nullable<System.DateTime> vertragsende { get; set; }
        public int tarifID { get; set; }
        public Nullable<decimal> datenvolumeninkl { get; set; }
        public Nullable<System.TimeSpan> telefonieinkl { get; set; }
        public Nullable<decimal> smsinkl { get; set; }
        public Nullable<decimal> grundpreis { get; set; }
        public Nullable<int> tarifoptionID1 { get; set; }
        public Nullable<int> tarifoptionID2 { get; set; }
        public Nullable<int> tarifoptionID3 { get; set; }
        public string vertragsnotiz { get; set; }
    
        public virtual Provider Provider { get; set; }
        public virtual Tarife Tarife { get; set; }
        public virtual Tarifoptionen Tarifoptionen { get; set; }
        public virtual Tarifoptionen Tarifoptionen1 { get; set; }
        public virtual Tarifoptionen Tarifoptionen2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Telefonnummern> Telefonnummern { get; set; }
        public virtual Verkaeufer Verkaeufer { get; set; }
        public virtual Vertragsarten Vertragsarten { get; set; }
    }
}
