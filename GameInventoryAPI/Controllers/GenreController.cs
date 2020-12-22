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
    [RoutePrefix("api/Genre")]
    public class GenreController : ApiController
    {
        private readonly GenreLogic genreLogic = new GenreLogic();

        #region GET
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

        [HttpGet, Route("Name/{genreName}")]
        public async Task<IHttpActionResult> GetGenreByName(string genreName)
        {
            try
            {
                return Ok(await genreLogic.GetGenreByNameAsync(genreName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<IHttpActionResult> Post(GenreModel genreModel)
        {
            try
            {
                if (genreModel == null) return BadRequest("Kein Genre zum hinzufügen");
                var checkName = await genreLogic.GetGenreByNameAsync(genreModel.Name);
                if (checkName != null) return BadRequest("Genre mit dem Namen " + genreModel.Name + " ist berist vorhanden");

                var checkInsert = await genreLogic.InsertGenreAsync(genreModel);
                if (checkInsert == null) return BadRequest("Fehler beim Insert");

                return Ok(checkInsert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, GenreModel genreModel)
        {
            try
            {
                if (id <= 0) return BadRequest("Keine Id");
                genreModel.GenreId = id;
                if (genreModel == null) return BadRequest("Kein Genre zum Updaten");

                var checkUpdate = await genreLogic.UpdateGenreAsync(genreModel);
                if (checkUpdate == null) return BadRequest("Fehler beim Upaten");

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
                var checkDelete = await genreLogic.DeleteGenreByIdAsync(id);

                if (!checkDelete) return BadRequest("Fehler beim löschen");
                return Ok("Genre erfolgreich gelöscht");
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

                var checkeDelete = await genreLogic.ForceDelteGenreByIdAsync(id);
                if (!checkeDelete) return BadRequest("Fehler beim löschen");

                return Ok("Force Delete erfolgreich");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}