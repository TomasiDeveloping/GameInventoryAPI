using GameInventoryAPI.Logic;
using GameInventoryAPI.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    [RoutePrefix("api/GameMode")]
    public class GameModeController : ApiController
    {
        private readonly GameModeLogic logic = new GameModeLogic();

        #region GET

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await logic.GetGameModesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var ganmeMode = await logic.GetGameModeByIdAsync(id);
                if (ganmeMode == null) return BadRequest("Game Modus mit der Id: " + id + " existiert nicht");
                return Ok(ganmeMode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion GET

        #region POST

        [HttpPost]
        public async Task<IHttpActionResult> Post(GameModeModel gameModeModel)
        {
            try
            {
                if (gameModeModel == null) return BadRequest("Kein Game Mode zum hinzufügen");
                var checkDuplicate = await logic.GetGameModeByNameAsyn(gameModeModel.Name);
                if (checkDuplicate != null) return BadRequest("Game Mode mit dem Namen" + gameModeModel.Name + " ist bereits vorhanden");

                var checkInsert = await logic.InsertGameModeAsync(gameModeModel);
                if (checkInsert == null) return BadRequest("Fehler beim hinzufügen");

                return Ok(checkInsert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion POST

        #region PUT

        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, GameModeModel gameModeModel)
        {
            try
            {
                if (gameModeModel == null) return BadRequest("Kein Game Mode zum Updaten");
                if (id <= 0) return BadRequest("Keine Id");
                gameModeModel.GameModeId = id;
                var checkUpdate = await logic.UpdateGameModeAsync(gameModeModel);

                if (checkUpdate == null) return BadRequest("Fehler beim Update");

                return Ok(checkUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion PUT

        #region DELETE

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("Keine Id");
                var checkDelete = await logic.DeleteGameModeById(id);

                if (!checkDelete) return BadRequest("Fehler beim löschen");
                return Ok("Game Mode erfolfreich gelöscht");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("{id}/ForceDelete")]
        public async Task<IHttpActionResult> ForceDelete(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("Keine Id");
                var checkDelete = await logic.ForceDeleteGameModeByIdAsync(id);

                if (!checkDelete) return BadRequest("Fehler beim löschen");

                return Ok("Force Delete erfolgreich");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion DELETE
    }
}