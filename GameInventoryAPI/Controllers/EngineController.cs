using GameInventoryAPI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    public class EngineController : BaseApiController
    {
        private readonly EngineLogic logic = new EngineLogic();

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
    }
}