using GameInventoryAPI.Logic;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    [RoutePrefix("api/Engine")]
    public class EngineController : ApiController
    {
        private readonly EngineLogic logic = new EngineLogic();

        #region GET
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await logic.GetEnginesAsync());
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
                var engine = await logic.GetEngineByIdAsync(id);
                if (engine == null) return BadRequest("Engine mit der Id: " + id + " existiert nicht");
                return Ok(engine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<IHttpActionResult> Post(GameEngineModel gameEngineModel)
        {
            try
            {
                if (gameEngineModel == null) return BadRequest("Keine Engin zum hinzufügen");
                var checkName = await logic.GetEngineByNameAsync(gameEngineModel.Name);
                if (checkName != null) return BadRequest("Engine mit dem Namen " + gameEngineModel.Name + " existiert bereits");

                var newGameEngine = await logic.InsertEngineAsync(gameEngineModel);

                if (newGameEngine == null) return BadRequest("Fehler beim hinzufügen");
                return Ok(newGameEngine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, GameEngineModel gameEngineModel)
        {
            try
            {
                if (id <= 0) return BadRequest("Keine Id angegben");
                gameEngineModel.GameEngineId = id;
                var engine = await logic.UpdateEngineAsync(gameEngineModel);
                if (engine == null) return BadRequest("Fehler beim Updaten");
                return Ok(engine);
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
                var check = await logic.DeleteEngineByIdAsync(id);

                if (check) return Ok(check);
                return BadRequest("Fehler beim löschen");
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
                var checkDelete = await logic.ForceDeleteEngineByIdAsync(id);

                if (!checkDelete) return BadRequest("Fehler beim löschen");

                return Ok("Engine erfolgreich gelöscht");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}