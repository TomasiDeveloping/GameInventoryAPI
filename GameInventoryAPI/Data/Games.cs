//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameInventoryAPI.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Games
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Games()
        {
            this.Game_GameMode = new HashSet<Game_GameMode>();
            this.Game_Genre = new HashSet<Game_Genre>();
            this.Game_Medium = new HashSet<Game_Medium>();
            this.Game_Plattform = new HashSet<Game_Plattform>();
        }
    
        public int GameId { get; set; }
        public string Name { get; set; }
        public int PublisherId { get; set; }
        public Nullable<int> GameEngineId { get; set; }
        public System.DateTime FirstPublication { get; set; }
        public string Description { get; set; }
        public int AgeRating { get; set; }
        public string Information { get; set; }
        public string CoverUrl { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_GameMode> Game_GameMode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Genre> Game_Genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Medium> Game_Medium { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Plattform> Game_Plattform { get; set; }
        public virtual GameEngines GameEngines { get; set; }
        public virtual Publishers Publishers { get; set; }
    }
}
