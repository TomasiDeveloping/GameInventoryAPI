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

        #region Get Functions
        public async Task<IEnumerable<GameModel>> GetGamesAsync()
        {
            var games = await gameRepository.GetGamesAsync();
            var models = new List<GameModel>();
            foreach (var game in games) models.Add(MapToModel(game));

            return models;
        }

        public async Task<IEnumerable<GameDto>> GetGamesDtoAsync()
        {
            return await gameRepository.GetGamesDtoAsync();
        }
        public async Task<IEnumerable<GameNameAndId>> GetGamesWithNameAndIdAsync()
        {
            return await gameRepository.GetGamesNameAndIdAsync();
        }

        public async Task<GameModel> GetGameByIdAsync(int gameId)
        {
            var game = await gameRepository.GetGameByIdAsync(gameId);
            if (game == null) return null;
            return MapToModel(game);
        }

        public async Task<IEnumerable<GameDto>> GetGamesByPlattformIdAsync(int plattformId)
        {
            return await gameRepository.GetGamesByPlattformIdAsync(plattformId);
            //var games = await gameRepository.GetGamesByPlattformIdAsync(plattformId);
            //var modelList = new List<GameModel>();
            //foreach (var item in games) modelList.Add(MapToModel(item));
            //return modelList;
        }

        public async Task<IEnumerable<GameModel>> GetGamesByPublisherIdAsync(int publisherId)
        {
            var games = await gameRepository.GetGamesByPublisherIdAsync(publisherId);
            var modelList = new List<GameModel>();
            foreach (var item in games) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<IEnumerable<GameDto>> GetGamesByMediumIdAsync(int mediumId)
        {
            return await gameRepository.GetGamesByMediumIdAsync(mediumId);
        }

        public async Task<IEnumerable<GameDto>> GetGamesByGenreIdAsync(int genreId)
        {
            return await gameRepository.GetGamesByGenreIdAsync(genreId);
        }

        public async Task<IEnumerable<GameDto>> GetGamesByGameModeIdAsync(int gameModeId)
        {
            return await gameRepository.GetGamesByGameModeIdAsync(gameModeId);
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

        public async Task<IEnumerable<GameDto>> GetGamesByFilterParamsAsync(GameFilterParams filterParams)
        {
            if (filterParams.PlattformId > 0 && filterParams.GenreId == 0 && filterParams.GameModeId == 0)
            {
                return await gameRepository.GetGamesByPlattformIdAsync(filterParams.PlattformId);
            }
            else if (filterParams.PlattformId == 0 && filterParams.GenreId > 0 && filterParams.GameModeId == 0)
            {
                return await gameRepository.GetGamesByGenreIdAsync(filterParams.GenreId);
            }
            else if (filterParams.PlattformId == 0 && filterParams.GenreId == 0 && filterParams.GameModeId > 0)
            {
                return await gameRepository.GetGamesByGameModeIdAsync(filterParams.GameModeId);
            }
            else if (filterParams.PlattformId > 0 && filterParams.GenreId > 0 && filterParams.GameModeId == 0)
            {
                return await gameRepository.GetGamesByPlattformAndGenreIdAsync(filterParams.PlattformId, filterParams.GenreId);
            }
            else if (filterParams.PlattformId > 0 && filterParams.GenreId == 0 && filterParams.GameModeId > 0)
            {
                return await gameRepository.GetGamesByPlattformAndGameModeIdAsync(filterParams.PlattformId, filterParams.GameModeId);
            }
            else if (filterParams.PlattformId == 0 && filterParams.GenreId > 0 && filterParams.GameModeId > 0)
            {
                return await gameRepository.GetGamesByGameModeAndGenreIdAsync(filterParams.GameModeId, filterParams.GenreId);
            }
            else if (filterParams.PlattformId > 0 && filterParams.GenreId > 0 && filterParams.GameModeId > 0)
            {
                return await gameRepository.GetGamesByPlatfformByGenreByGameModeIdAsync(filterParams.PlattformId, filterParams.GenreId, filterParams.GameModeId);
            }
            else
            {
                return null;
            }
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

        #region Helper
        public async Task<bool> CheckPlattformExists(int gameId, int plattformId)
        {
            return await gameRepository.CheckPlattformExistsAsync(gameId, plattformId);
        }

        public async Task<bool> CheckGameModeExitsAsync(int gameId, int gameModeId)
        {
            return await gameRepository.CheckGameModeExistsAsync(gameId, gameModeId);
        }

        public async Task<bool> CheckGenreExitst(int gameId, int genreId)
        {
            return await gameRepository.CheckGenreExistsAsync(gameId, genreId);
        }

        public async Task<bool> CheckMediumExitsAsync(int gameId, int mediumId)
        {
            return await gameRepository.CheckMediumExits(gameId, mediumId);
        }
        #endregion

        private GameModel MapToModel(Games game)
        {
            return new GameModel
            {
                GameId = game.GameId,
                Name = game.Name,
                GameEngineId = game.GameEngineId,
                PublisherName = game.Publishers.Name,
                PublisherId = game.PublisherId,
                AgeRating = game.AgeRating,
                CoverUrl = game.CoverUrl,
                Description = game.Description,
                FirstPublication = game.FirstPublication,
                GameEngineName = game.GameEngines?.Name,
                Information = game.Information,
                Plattforms = game.Game_Plattform?.Select(p => p.Plattform.Name),
                Genres = game.Game_Genre?.Select(g => g.Genres.Name),
                Mediums =game.Game_Medium?.Select(m => m.Medium.Name),
                GameModes = game.Game_GameMode?.Select(m => m.GameMode.Name),
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