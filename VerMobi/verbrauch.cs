//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VerMobi
{
    using System;
    using System.Collections.Generic;
    
    public partial class verbrauch
    {
        public int verbrauchID { get; set; }
        public int telnrID { get; set; }
        public System.DateTime verbrauchbeginn { get; set; }
        public Nullable<System.DateTime> verbrauchende { get; set; }
        public Nullable<decimal> datenvolumeninkl { get; set; }
        public Nullable<decimal> datenverbrauch { get; set; }
        public Nullable<System.TimeSpan> telefonieinkl { get; set; }
        public Nullable<System.TimeSpan> telefonieverbrauch { get; set; }
        public Nullable<decimal> smsinkl { get; set; }
        public Nullable<decimal> smsverbrauch { get; set; }
        public Nullable<decimal> grundpreis { get; set; }
        public Nullable<decimal> rgbetrag { get; set; }
        public string verbrauchnotiz { get; set; }
    
        public virtual telefonnummern telefonnummern { get; set; }
    }
}
