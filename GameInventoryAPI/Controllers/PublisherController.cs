using GameInventoryAPI.Logic;
using GameInventoryAPI.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    [RoutePrefix("api/Publisher")]
    public class PublisherController : ApiController
    {
        private readonly PublisherLogic publisherLogic = new PublisherLogic();

        #region GET

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await publisherLogic.GetPublisherAsync());
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
                var publisher = await publisherLogic.GetPublisherByIdAsync(id);
                if (publisher == null) return BadRequest("Publisher mit der Id: " + id + " nicht vorhanden");
                return Ok(publisher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("Name/{publisherName}")]
        public async Task<IHttpActionResult> GetPublisherByName(string publisherName)
        {
            try
            {
                return Ok(await publisherLogic.GetPublisherByNameAsync(publisherName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion GET

        #region POST

        [HttpPost]
        public async Task<IHttpActionResult> Post(PublisherModel publisherModel)
        {
            try
            {
                if (publisherModel == null) return BadRequest("Kein Publisher zum hinzufügen");
                var checkName = await publisherLogic.GetPublisherByNameAsync(publisherModel.Name);

                if (checkName != null) return BadRequest("Publisher mit dem Namen " + publisherModel.Name + " existiert bereits");

                var checkInsert = await publisherLogic.InsertPublisherAsync(publisherModel);
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
        public async Task<IHttpActionResult> Put(int id, PublisherModel publisherModel)
        {
            try
            {
                if (id <= 0) return BadRequest("Keine Id");
                publisherModel.PublisherId = id;
                if (publisherModel == null) return BadRequest("Kein Publsiher zum updaten");

                var checkUpdate = await publisherLogic.UpdatePublisherAsync(publisherModel);
                if (checkUpdate == null) return BadRequest("Fehler beim update");

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

                var checkDelete = await publisherLogic.DeletePublisherByIdAsync(id);
                if (!checkDelete) return BadRequest("Fehler beim löschen");

                return Ok("Publisher erfolgreich gelöscht");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion DELETE
    }
}