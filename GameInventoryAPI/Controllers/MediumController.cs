using GameInventoryAPI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    [RoutePrefix("api/Medium")]
    public class MediumController : ApiController
    {
        private readonly MediumLogic logic = new MediumLogic();

        #region GET
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await logic.GetMediumsAsync());
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
                var genre = await logic.GetMediumByIdAsync(id);
                if (genre == null) return BadRequest("Medium mit der Id: " + id + " existiert nicht");
                return Ok(genre);
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