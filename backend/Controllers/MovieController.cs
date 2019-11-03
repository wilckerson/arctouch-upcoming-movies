using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ArcTouch.UpcomingMovies.Api.BLL;
using ArcTouch.UpcomingMovies.Api.Models;
using ArcTouch.UpcomingMovies.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace ArcTouch.UpcomingMovies.Api.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly MovieBLL movieBLL;

        public MovieController(IMovieService movieService, IMemoryCache cacheService, IConfiguration configuration)
        {
            movieBLL = new MovieBLL(movieService, cacheService, configuration);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies(string query = null, int? page = null)
        {
            try
            {
                var result = await movieBLL.GetMovies(query, page);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetMovieGenres()
        {
            try
            {
                var result = await movieBLL.GetMovieGenres();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            try
            {
                var result = await movieBLL.GetMovieDetails(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}