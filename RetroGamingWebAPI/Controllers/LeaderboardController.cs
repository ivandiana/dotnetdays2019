﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using RetroGamingWebAPI.Infrastructure;
using RetroGamingWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGamingWebAPI.Controllers
{
    [OpenApiTag("Leaderboard", Description = "API to retrieve high score leaderboard")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly RetroGamingContext context;

        public LeaderboardController(RetroGamingContext context)
        {
            this.context = context;
        }

        // GET api/leaderboard
        /// <summary>
        /// Retrieve a list of leaderboard scores.
        /// </summary>
        /// <returns>List of high scores per game.</returns>
        /// <response code="200">The list was successfully retrieved.</response>
        [ProducesResponseType(typeof(IEnumerable<HighScore>), 200)]
        [Produces("application/json", "application/xml")]
        [HttpGet("{format?}")]
        [FormatFilter]
        public async Task<ActionResult<IEnumerable<HighScore>>> Get()
        {
            var scores = context.Scores
               .Select(score => new HighScore()
               {
                   Game = score.Game,
                   Points = score.Points,
                   Nickname = score.Gamer.Nickname
               });

            return Ok(await scores.ToListAsync().ConfigureAwait(false));
        }
    }
}