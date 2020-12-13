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
        [HttpPost]
        public async Task<IHttpActionResult> Post(PlattformModel plattformModel)
        {
            try
            {
                if (plattformModel == null) return BadRequest("Keine Plattform zum hinzufügen");
                var checkName = await logic.GetPlattformByNameAsync(plattformModel.Name);
                if (checkName != null) return BadRequest("Plattform mit dem Namen " + plattformModel.Name + " existriert bereits");

                var checkInsert = await logic.InsertPlattformAsync(plattformModel);
                if (checkInsert == null) return BadRequest("Fehler beim hinzufügen");

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
        public async Task<IHttpActionResult> Put(int id, PlattformModel plattformModel)
        {
            try
            {
                if (id <= 0) return BadRequest("Keine Id");
                plattformModel.PlattformId = id;
                if (plattformModel == null) return BadRequest("Keine Plafftorm zum aktuallisieren");

                var checkUpdate = await logic.UpdatePlattformAsync(plattformModel);

                if (checkUpdate == null) return BadRequest("Fehler beim Update");

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
                var checkDelete = await logic.DeletePlattformByIdAsync(id);

                if (!checkDelete) return BadRequest("Fehler beim löschen");

                return Ok("Plattform erfolgreich gelöscht");
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

                var checkDelete = await logic.ForceDeletePlattformByIdAsync(id);

                if (!checkDelete) return BadRequest("Fehler beim löschen");

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