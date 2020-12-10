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
                var check = await logic.InsertEngineAsync(gameEngineModel);

                if (check) return Ok(gameEngineModel);
                return BadRequest("Fehler beim hinzufügen");
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

                if (check) return Ok();
                return BadRequest("Fehler beim löschen");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
     
        }
        #endregion
    }
}