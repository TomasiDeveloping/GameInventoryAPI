﻿using GameInventoryAPI.Data;
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
    [RoutePrefix("api/Game")]
    public class GameController : ApiController
    {
        private readonly GameLogic gameLogic = new GameLogic();

        #region GET
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

        [HttpGet, Route("Plattform/{plattformId}")]
        public async Task<IHttpActionResult> GetGamesByPlattformId(int plattformId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByPlattformIdAsync(plattformId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("Publisher/{publisherId}")]
        public async Task<IHttpActionResult> GetGamesByPublisherId(int publisherId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByPublisherIdAsync(publisherId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("Medium/{mediumId}")]
        public async Task<IHttpActionResult> GetGamesByMediumId(int mediumId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByMediumIdAsync(mediumId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("Genre/{genreId}")]
        public async Task<IHttpActionResult> GetGamesByGenreId(int genreId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByGenreIdAsync(genreId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GameMode/{gameModeId}")]
        public async Task<IHttpActionResult> GetGamesByGameModeId(int gameModeId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByGameModeIdAsync(gameModeId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("Engine/{engineId}")]
        public async Task<IHttpActionResult> GetGamesByEngineId(int engineId)
        {
            try
            {
                return Ok(await gameLogic.GetGamesByEngineIdAsync(engineId));
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
