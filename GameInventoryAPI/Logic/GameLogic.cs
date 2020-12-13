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
        private readonly PublisherRepository publisherRepository = new PublisherRepository();
        private readonly EngineRepository engineRepository = new EngineRepository();

        #region Get Functions
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

        public async Task<GameModel> GetGameByNameAsync(string gameName)
        {
            var game = await gameRepository.GetGameByNameAsync(gameName);
            if (game == null) return null;
            return MapToModel(game);
        }
        #endregion

        #region Update Functions
        public async Task<GameModel> UpdateGameAsync(GameModel gameModel)
        {
            if (gameModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await gameRepository.UpdateGameAsync(MapToDbObject(gameModel)));
        }
        #endregion

        #region Insert Functions
        public async Task<GameModel> InsertGameAsync(GameModel gameModel)
        {
            if (gameModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await gameRepository.InsertGameAsync(MapToDbObject(gameModel)));
        }

        public async Task<bool> InsertPlattformToGame(int gameId, int plattformId)
        {
            return await gameRepository.InsertPlattformToGameAsync(gameId, plattformId);
        }

        public async Task<bool> InsertGenreToGame(int gameId, int genreId)
        {
            return await gameRepository.InsertGenreToGameAsync(gameId, genreId);
        }

        public async Task<bool> InsertMediumToGame(int gameId, int mediumId)
        {
            return await gameRepository.InsertMediumToGameAsync(gameId, mediumId);
        }

        public async Task<bool> InsertGameModeToGame(int gameId, int gameModeId)
        {
            return await gameRepository.InsertGameModeToGameAsync(gameId, gameModeId);
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeleteGameByIdAsync(int gameId)
        {
            return await gameRepository.DeleteGameByIdAsync(gameId);
        }

        public async Task<bool> RemovePlattformFromGame(int gameId, int plattformId)
        {
            return await gameRepository.RemovePlattformFromGame(gameId, plattformId);
        }

        public async Task<bool> RemoveGenreFromGame(int gameId, int genreId)
        {
            return await gameRepository.RemoveGenreFromGameAsync(gameId, genreId);
        }

        public async Task<bool> RemoveMediumFromGame(int gameId, int mediumId)
        {
            return await gameRepository.RemoveMediumFromGameAsync(gameId, mediumId);
        }

        public async Task<bool> RemoveGameModeFromGame(int gameId, int gameModeId)
        {
            return await gameRepository.RemoveGameModeFromGameAsync(gameId, gameModeId);
        }
        #endregion

        private GameModel MapToModel(Games game)
        {
            return new GameModel
            {
                GameId = game.GameId,
                Name = game.Name,
                GameEngineId = game.GameEngineId,
                PublisherName = publisherRepository.GetPublisherNameById(game.PublisherId),
                PublisherId = game.PublisherId,
                AgeRating = game.AgeRating,
                CoverUrl = game.CoverUrl,
                Description = game.Description,
                FirstPublication = game.FirstPublication,
                GameEngineName = game.GameEngineId.HasValue ? engineRepository.GetEngineNameById(game.GameEngineId.Value) : "",
                Information = game.Information,
                Plattforms = game.Game_Plattform?.Select(p => new GameModelPlattform { PlattformName = p.Plattform.Name }).ToList(),
                GameModes = game.Game_GameMode?.Select(m => new GameModelGameMode { GameModeName = m.GameMode.Name }).ToList(),
                Genres = game.Game_Genre?.Select(g => new GameModelGenre { GenreName = g.Genres.Name }).ToList(),
                Mediums = game.Game_Medium?.Select(m => new GameModelMedium { MediumName = m.Medium.Name }).ToList()
            };
        }

        private Games MapToDbObject(GameModel gameModel)
        {
            if (gameModel == null) return null;

            return new Games
            {
                GameId = gameModel.GameId,
                Name = gameModel.Name,
                PublisherId = gameModel.PublisherId,
                GameEngineId = gameModel.GameEngineId,
                AgeRating = gameModel.AgeRating,
                CoverUrl = gameModel.CoverUrl,
                Description = gameModel.Description,
                FirstPublication = gameModel.FirstPublication,
                Information = gameModel.Information,
            };
        }
    }
}