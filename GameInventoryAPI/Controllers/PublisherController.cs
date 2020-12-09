using GameInventoryAPI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    public class PublisherController : BaseApiController
    {
        private readonly PublisherLogic publisherLogic = new PublisherLogic();

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await publisherLogic.GetPublisherAsync());
            }
            catch(Exception ex)
            {
              return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var publisher = await publisherLogic.GetPublisherByIdAsync(id);
                if (publisher == null) return BadRequest("Publisher mit der Id: " + id + " nicht vorhanden");
                return Ok(publisher);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}