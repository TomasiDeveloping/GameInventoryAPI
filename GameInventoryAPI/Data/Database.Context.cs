﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GameInventoryEntities : DbContext
    {
        public GameInventoryEntities()
            : base("name=GameInventoryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Game_GameMode> Game_GameMode { get; set; }
        public virtual DbSet<Game_Genre> Game_Genre { get; set; }
        public virtual DbSet<Game_Medium> Game_Medium { get; set; }
        public virtual DbSet<Game_Plattform> Game_Plattform { get; set; }
        public virtual DbSet<GameEngines> GameEngines { get; set; }
        public virtual DbSet<GameMode> GameMode { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Medium> Medium { get; set; }
        public virtual DbSet<Plattform> Plattform { get; set; }
        public virtual DbSet<Publishers> Publishers { get; set; }
    }
}
