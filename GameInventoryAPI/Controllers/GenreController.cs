using GameInventoryAPI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    [RoutePrefix("api/Genre")]
    public class GenreController : ApiController
    {
        private readonly GenreLogic genreLogic = new GenreLogic();

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await genreLogic.GetGenresAsync());
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
                var genre = await genreLogic.GetGenreByIdAsync(id);
                if (genre == null) return BadRequest("Genre mit der Id: " + id + " existiert nicht");
                return Ok(genre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}