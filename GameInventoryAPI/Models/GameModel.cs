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
        public IEnumerable<GameModelMedium> Mediums { get; set; }
        public IEnumerable<GameModelGenre> Genres { get; set; }
        public IEnumerable<GameModelPlattform> Plattforms { get; set; }
        public IEnumerable<GameModelGameMode> GameModes { get; set; }
    }

    public class GameModelMedium
    {
        public int MediumId { get; set; }
        public string MediumName { get; set; }
    }

    public class GameModelGenre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }

    public class GameModelPlattform
    {
        public int PlattformId { get; set; }
        public string PlattformName { get; set; }
    }

    public class GameModelGameMode
    {
        public int GameModeId { get; set; }
        public string GameModeName { get; set; }
    }

    public class GameNameAndId
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
    }

    public class GameDto
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string CoverUrl { get; set; }
        public string PublisherName { get; set; }
        public DateTime FirstPublication { get; set; }
    }

    public class GameFilterParams
    {
        public int PlattformId { get; set; }
        public int GenreId { get; set; }
        public int GameModeId { get; set; }
    }
}