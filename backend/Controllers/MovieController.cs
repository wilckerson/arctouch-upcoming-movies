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

namespace ArcTouch.UpcomingMovies.Api.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly MovieBLL movieBLL;
        private readonly IMemoryCache cache;

        public MovieController(IMovieService movieService, IMemoryCache cache)
        {
            movieBLL = new MovieBLL(movieService);
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies(string query = null, int? page = null)
        {
            try
            {
                var cacheResult = await cache.GetOrCreateAsync($"getmovies_{page}_{Sanitize(query)}", (context) =>
                {
                    context.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                    context.SetPriority(CacheItemPriority.High);

                    var result = movieBLL.GetMovies(query, page);
                    return result;
                });
                return Ok(cacheResult);
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
                var cacheResult = await cache.GetOrCreateAsync("getmoviegenres", (context) =>
                {
                    context.SetAbsoluteExpiration(TimeSpan.FromMinutes(7));
                    context.SetPriority(CacheItemPriority.High);

                    var result = movieBLL.GetMovieGenres();
                    return result;
                });
                return Ok(cacheResult);
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
                var cacheResult = await cache.GetOrCreateAsync($"getmoviedetails_{id}", (context) =>
                {
                    context.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                    context.SetPriority(CacheItemPriority.High);

                    var result = movieBLL.GetMovieDetails(id);
                    return result;
                });
                return Ok(cacheResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private static string Sanitize(string s)
        {
            if (string.IsNullOrEmpty(s)) { return null; }

            return String.Join("", s.AsEnumerable()
                                    .Select(chr => Char.IsLetter(chr) || Char.IsDigit(chr)
                                                   ? chr.ToString()      // valid symbol
                                                   : "_" + (short)chr + "_") // numeric code for invalid symbol
                              );
        }
    }
}