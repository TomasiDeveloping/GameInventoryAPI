using GameInventoryAPI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    [RoutePrefix("api/Plattform")]
    public class PlattformController : ApiController
    {
        private readonly PlattformLogic logic = new PlattformLogic();

        #region GET
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await logic.GetPlattformsAsync());
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
                var plattform = await logic.GetPlattformByIdAsync(id);
                if (plattform == null) return BadRequest("Plattform mit der Id: " + id + " existiert nicht");
                return Ok(plattform);
            }
            catch(Exception ex)
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