using GameInventoryAPI.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameInventoryAPI.Models
{
    public class GameModel
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public int? GameEngineId { get; set; }
        public string GameEngineName { get; set; }
        public DateTime FirstPublication { get; set; }
        public string Description { get; set; }
        public int AgeRating { get; set; }
        public string Information { get; set; }
        public string CoverUrl { get; set; }
        public ICollection<GameModelMedium> Mediums { get; set; }
        public ICollection<GameModelGenre> Genres { get; set; }
        public ICollection<GameModelPlattform> Plattforms { get; set; }
        public ICollection<GameModelGameMode> GameModes { get; set; }
    }

    public class GameModelGenre
    {
        public string GenreName { get; set; }
    }

    public class GameModelPlattform
    {
        public string PlattformName { get; set; }
    }

    public class GameModelMedium
    {
        public string MediumName { get; set; }
    }
    public class GameModelGameMode
    {
        public string GameModeName { get; set; }
    }
}