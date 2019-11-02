using ArcTouch.UpcomingMovies.Api.Models;
using ArcTouch.UpcomingMovies.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTouch.UpcomingMovies.Api.BLL
{
    public class MovieBLL
    {
        private readonly IMovieService movieService;

        public MovieBLL(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<PaginatedMovieList> GetMovies(string query = null, int? page = null)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var result = await movieService.SearchMovies(query, page);
                return result;
            }
            else
            {
                var result = await movieService.GetUpcomingMovies(page);
                return result;
            }
        }

        public async Task<Movie> GetMovieDetails(int id)
        {
            var result = await movieService.GetMovieDetails(id);
            return result;
        }

        public async Task<IEnumerable<MovieGenre>> GetMovieGenres()
        {
            var result = await movieService.GetMovieGenres();
            return result;
        }
    }
}
