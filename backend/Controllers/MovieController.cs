using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcTouch.UpcomingMovies.Api.BLL;
using ArcTouch.UpcomingMovies.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArcTouch.UpcomingMovies.Api.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly MovieBLL movieBLL;

        public MovieController(IMovieService movieService)
        {
            movieBLL = new MovieBLL(movieService);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies(string query = null, int? page = null)
        {
            try
            {
                var result = await movieBLL.GetMovies(query,page);
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
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}