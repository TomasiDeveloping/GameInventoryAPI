using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Logic;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GameInventoryAPI.Controllers
{
    public class GameController : BaseApiController
    {
        private readonly GameLogic gameLogic = new GameLogic();

        [HttpGet]
        public async Task <IHttpActionResult> Get()
        {
          
            return Ok(await gameLogic.GetGamesAsync());
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var game = await gameLogic.GetGameByIdAsync(id);

                if (game == null) return BadRequest("Game mit der Id: " + id + " wurde nicht gefunden");
                return Ok(game);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
      
        }
    }
}
