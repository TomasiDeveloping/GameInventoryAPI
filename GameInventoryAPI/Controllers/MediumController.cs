using GameInventoryAPI.Logic;
using GameInventoryAPI.Models;
using System;
using System.Threading.Tasks;
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

        #endregion GET

        #region POST

        [HttpPost]
        public async Task<IHttpActionResult> Post(MediumModel mediumModel)
        {
            try
            {
                if (mediumModel == null) return BadRequest("Kein Medium zum hinzufügen");
                var checkName = await logic.GetMediumByNameAsync(mediumModel.Name);
                if (checkName != null) return BadRequest("Medium mit dem Namen " + mediumModel.Name + " exisitiert bereits");

                var checkInsert = await logic.InsertMediumAsync(mediumModel);

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
        public async Task<IHttpActionResult> Put(int id, MediumModel mediumModel)
        {
            try
            {
                if (id <= 0) return BadRequest("Keine Id");
                mediumModel.MediumId = id;
                if (mediumModel == null) return BadRequest("Kein Medium zum Updaten");

                var checkUpdate = await logic.UpdateMediumAsync(mediumModel);
                if (checkUpdate == null) return BadRequest("Fehler beim Updaten");

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
                var checkDelete = await logic.DeleteMediumByIdAsync(id);

                if (!checkDelete) return BadRequest("Fehler beim löschen");

                return Ok("Medium erfolgreich gelöscht");
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
                var checkDelete = await logic.ForceDeleteMediumByIdAsync(id);
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