using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Logic;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    [RoutePrefix("api/Game")]
    public class GameController : ApiController
    {
        private readonly GameLogic gameLogic = new GameLogic();

        #region GET
        [HttpGet]
        public async Task <IHttpActionResult> Get()
        {
          
            return Ok(await gameLogic.GetGamesAsync());
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var game = await gameLogic.GetGameByIdAsync(id);

                if (game == null) return BadRequest("Game mit der Id: " + id + " wurde nicht gefunden");
                return Ok(game);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
      
        }

        [HttpGet, Route("Plattform/{plattformId}")]
        public async Task<IHttpActionResult> GetGamesByPlattformId(int plattformId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByPlattformIdAsync(plattformId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("Publisher/{publisherId}")]
        public async Task<IHttpActionResult> GetGamesByPublisherId(int publisherId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByPublisherIdAsync(publisherId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("Medium/{mediumId}")]
        public async Task<IHttpActionResult> GetGamesByMediumId(int mediumId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByMediumIdAsync(mediumId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("Genre/{genreId}")]
        public async Task<IHttpActionResult> GetGamesByGenreId(int genreId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByGenreIdAsync(genreId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GameMode/{gameModeId}")]
        public async Task<IHttpActionResult> GetGamesByGameModeId(int gameModeId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByGameModeIdAsync(gameModeId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("Engine/{engineId}")]
        public async Task<IHttpActionResult> GetGamesByEngineId(int engineId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByEngineIdAsync(engineId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<IHttpActionResult> Post(GameModel gameModel)
        {
            try
            {
                if (gameModel == null) return BadRequest("Kein Game zum hinzufügen");
                var checkName = await gameLogic.GetGameByNameAsync(gameModel.Name);

                if (checkName != null) return BadRequest("Game mit dem Namen " + gameModel.Name + " existiert bereits");

                var checkInsert = await gameLogic.InsertGameAsync(gameModel);
                if (checkInsert == null) return BadRequest("Fehler beim hinzufügen");

                return Ok(checkInsert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("{gameId}/AddPlattform/{plattformId}")]
        public async Task<IHttpActionResult> AddPlattformToGame(int gameId, int plattformId)
        {
            try
            {
                if (gameId <= 0 || plattformId <= 0) return BadRequest("Fehler mit IDs");

                var checkAddPlattform = await gameLogic.InsertPlattformToGame(gameId, plattformId);
                if (!checkAddPlattform) return BadRequest("Fehler beim hinzufügen der Plattform");

                return Ok("Plattform erfolgreich hinzugefügt");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("{gameId}/AddMedium/{mediumId}")]
        public async Task<IHttpActionResult> AddMediumToGame(int gameId, int mediumId)
        {
            try
            {
                if (gameId <= 0 || mediumId <= 0) return BadRequest("Ids Fehler");

                var checkAddMedium = await gameLogic.InsertMediumToGame(gameId, mediumId);
                if (!checkAddMedium) return BadRequest("Fehler: Medium konnte nicht hinzugefügt werden");

                return Ok("Medium erfolgreich hinzugefügt");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("{gameId}/AddGenre/{genreId}")]
        public async Task<IHttpActionResult> AddGenreToGame(int gameId, int genreId)
        {
            try
            {
                if (gameId <= 0 || genreId <= 0) return BadRequest("Ids Fehler");

                var checkAddGenre = await gameLogic.InsertGenreToGame(gameId, genreId);
                if (!checkAddGenre) return BadRequest("Fehler: Genre konnte nicht hinzugefügt werden");

                return Ok("Genre erfolgreich hinzugefügt");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost, Route("{gameId}/AddGameMode/{gameModeId}")]
        public async Task<IHttpActionResult> AddGameModeToGame(int gameId, int gameModeId)
        {
            try
            {
                if (gameId <= 0 || gameModeId <= 0) return BadRequest("Ids Fehler");

                var checkAddGameMode = await gameLogic.InsertGameModeToGame(gameId, gameModeId);
                if (!checkAddGameMode) return BadRequest("Fehler: Game Mode konnte nicht hinzugefügt werden");

                return Ok("Game Mode erfolgreich hinzugefügt");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, GameModel gameModel)
        {
            try
            {
                if (id <= 0) return BadRequest("Keine Id");
                if (gameModel == null) return BadRequest("Kein Game zum Updaten");

                var checkUpdate = await gameLogic.UpdateGameAsync(gameModel);
                if (checkUpdate == null) return BadRequest("Fehler beim Updaten");

                return Ok(checkUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("Keine Id");

                var checkDelete = await gameLogic.DeleteGameByIdAsync(id);

                if (!checkDelete) return BadRequest("Fehler beim löschen");

                return Ok("Game erfolgreich gelöscht");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete, Route("{gameId}/RemovePlattform/{plattformId}")]
        public async Task<IHttpActionResult> RemovePlattformFromGame(int gameId, int plattformId)
        {
            try
            {
                if (gameId <= 0 || plattformId <= 0) return BadRequest("Ids Fehler");
                var checkRemove = await gameLogic.RemovePlattformFromGame(gameId, plattformId);

                if (!checkRemove) return BadRequest("Plattform konnte nicht enternt werden");

                return Ok("Plattform erfolgreich entfernt");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("{gameId}/RemoveMedium/{mediumId}")]
        public async Task<IHttpActionResult> RemoveMediumFromGame(int gameId, int mediumId)
        {
            try
            {
                if (gameId <= 0 || mediumId <= 0) return BadRequest("Ids Fehler");
                var checkRemoveMedium = await gameLogic.RemoveMediumFromGame(gameId, mediumId);

                if (!checkRemoveMedium) return BadRequest("Fehler: Medium konnte nicht entfernt werden");

                return Ok("Medium erfolgreich entfernt");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("{gameId}/RemoveGenre/{genreId}")]
        public async Task<IHttpActionResult> RemoveGenreFromGame(int gameId, int genreId)
        {
            try
            {
                if (gameId <= 0 || genreId <= 0) return BadRequest("Ids Fehler");

                var checkRemoveGenre = await gameLogic.RemoveGenreFromGame(gameId, genreId);
                if (!checkRemoveGenre) return BadRequest("Fehler: Genre konnte nicht entfernt werden");

                return Ok("Genre erfolgreich entfernt");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("{gameId}/RemoveGameMode/{gameModeId}")]
        public async Task<IHttpActionResult> RemoveGameModeFromGame(int gameId, int gameModeId)
        {
            try
            {
                if (gameId <= 0 || gameModeId <= 0) return BadRequest("Ids Fehler");

                var checkRemoveGameMode = await gameLogic.RemoveGameModeFromGame(gameId, gameModeId);
                if (!checkRemoveGameMode) return BadRequest("Fehler: Game Mode konnte nicht entfernt werden");

                return Ok("Game Mode erfolgreich entfernt");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
