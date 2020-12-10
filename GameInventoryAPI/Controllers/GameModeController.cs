using GameInventoryAPI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        #endregion

        #region POST
        #endregion

        #region PUT
        #endregion

        #region DELETE
        #endregion
    }
}