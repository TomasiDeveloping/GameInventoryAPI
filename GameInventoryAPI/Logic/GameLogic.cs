using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Logic
{
    public class GameLogic
    {
        private readonly GameRepository gameRepository = new GameRepository();
        public async Task<IEnumerable<GameModel>> GetGamesAsync()
        {
            var games = await gameRepository.GetGamesAsync();
            var models = new List<GameModel>();
            foreach (var game in games) models.Add(MapToModel(game));

            return models;
        }

        public async Task<GameModel> GetGameByIdAsync(int gameId)
        {
            var game = await gameRepository.GetGameByIdAsync(gameId);
            if (game == null) return null;
            return MapToModel(game);
        }

        public async Task<IEnumerable<GameModel>> GetGamesByPlattformIdAsync(int plattformId)
        {
            var games = await gameRepository.GetGamesByPlattformIdAsync(plattformId);
            var modelList = new List<GameModel>();
            foreach (var item in games) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<IEnumerable<GameModel>> GetGamesByPublisherIdAsync(int publisherId)
        {
            var games = await gameRepository.GetGamesByPublisherIdAsync(publisherId);
            var modelList = new List<GameModel>();
            foreach (var item in games) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<IEnumerable<GameModel>> GetGamesByMediumIdAsync(int mediumId)
        {
            var games = await gameRepository.GetGamesByMediumIdAsync(mediumId);
            var modelList = new List<GameModel>();
            foreach (var item in games) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<IEnumerable<GameModel>> GetGamesByGenreIdAsync(int genreId)
        {
            var games = await gameRepository.GetGamesByGenreIdAsync(genreId);
            var modelList = new List<GameModel>();
            foreach (var item in games) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<IEnumerable<GameModel>> GetGamesByGameModeIdAsync(int gameModeId)
        {
            var games = await gameRepository.GetGamesByGameModeIdAsync(gameModeId);
            var modelList = new List<GameModel>();
            foreach (var item in games) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<IEnumerable<GameModel>> GetGamesByEngineIdAsync(int engineId)
        {
            var games = await gameRepository.GetGamesByEngineIdAsync(engineId);
            var modelList = new List<GameModel>();
            foreach (var item in games) modelList.Add(MapToModel(item));
            return modelList;
        }

        public GameModel MapToModel(Games game)
        {
            return new GameModel
            {
                GameId = game.GameId,
                Name = game.Name,
                PublisherName = game.Publishers.Name,
                AgeRating = game.AgeRating,
                CoverUrl = game.CoverUrl,
                Description = game.Description,
                FirstPublication = game.FirstPublication,
                GameEngineName = game.GameEngines?.Name,
                Information = game.Information,
                Plattforms = game.Game_Plattform?.Select(p => new GameModelPlattform { PlattformName = p.Plattform.Name }).ToList(),
                GameModes = game.Game_GameMode?.Select(m => new GameModelGameMode { GameModeName = m.GameMode.Name }).ToList(),
                Genres = game.Game_Genre?.Select(g => new GameModelGenre { GenreName = g.Genres.Name }).ToList(),
                Mediums = game.Game_Medium?.Select(m => new GameModelMedium { MediumName = m.Medium.Name }).ToList()
            };
        }
    }
}