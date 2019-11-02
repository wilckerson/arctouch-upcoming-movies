using ArcTouch.UpcomingMovies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTouch.UpcomingMovies.Api.Services
{
    public interface IMovieService
    {
        Task<PaginatedMovieList> GetUpcomingMovies();
        Task<PaginatedMovieList> SearchMovies(string query);
        Task<Movie> GetMovieDetails(int id);
    }
}
