using ArcTouch.UpcomingMovies.Api.Models;
using ArcTouch.UpcomingMovies.Api.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTouch.UpcomingMovies.Api.BLL
{
    public class MovieBLL
    {
        
        private readonly IConfiguration configuration;
        private readonly IMovieService movieService;
        private readonly IMemoryCache cacheService;

        public MovieBLL(IMovieService movieService, IMemoryCache cacheService, IConfiguration configuration)
        {
            this.movieService = movieService;
            this.cacheService = cacheService;
            this.configuration = configuration;
        }

        public async Task<PaginatedMovieList> GetMovies(string query = null, int? page = null)
        {
            var cacheResult = await cacheService.GetOrCreateAsync($"getmovies_{page}_{Sanitize(query)}", (context) =>
            {
                var minutes = configuration.GetSection("CacheExpiration").GetValue<int>("GetMoviesMinutes");
                context.SetAbsoluteExpiration(TimeSpan.FromMinutes(minutes));
                context.SetPriority(CacheItemPriority.High);

                if (!string.IsNullOrEmpty(query))
                {
                    var result = movieService.SearchMovies(query, page);
                    return result;
                }
                else
                {
                    var result = movieService.GetUpcomingMovies(page);
                    return result;
                }
            });

            return cacheResult;            
        }

        public async Task<Movie> GetMovieDetails(int id)
        {
            var cacheResult = await cacheService.GetOrCreateAsync($"getmoviedetails_{id}", (context) =>
            {
                var minutes = configuration.GetSection("CacheExpiration").GetValue<int>("GetMovieDetailsMinutes");
                context.SetAbsoluteExpiration(TimeSpan.FromMinutes(minutes));
                context.SetPriority(CacheItemPriority.High);

                var result = movieService.GetMovieDetails(id);
                return result;
            });

            return cacheResult;
        }

        public async Task<IEnumerable<MovieGenre>> GetMovieGenres()
        {
            var cacheResult = await cacheService.GetOrCreateAsync("getmoviegenres", (context) =>
            {
                var minutes = configuration.GetSection("CacheExpiration").GetValue<int>("GetGenresMinutes");
                context.SetAbsoluteExpiration(TimeSpan.FromMinutes(minutes));
                context.SetPriority(CacheItemPriority.High);

                var result = movieService.GetMovieGenres();
                return result;                
            });

            return cacheResult;
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
