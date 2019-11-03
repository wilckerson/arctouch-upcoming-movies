using ArcTouch.UpcomingMovies.Api.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArcTouch.UpcomingMovies.Api.Services
{
    public class TheMovieDbService: IMovieService
    {
        const string API_URL = "https://api.themoviedb.org/3";
        private readonly string apiKey;

        public TheMovieDbService(string apiKey)
        {
            this.apiKey = apiKey;
        }
        public async Task<PaginatedMovieList> GetUpcomingMovies(int? page = null)
        {
            using (HttpClient http = new HttpClient())
            {
                var response = await http.GetAsync($"{API_URL}/movie/upcoming?api_key={apiKey}&page={page.GetValueOrDefault(1)}");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var erro = $"StatusCode:{response.StatusCode} Message: {response.ReasonPhrase} Response: {responseContent}";
                    throw new Exception("Error on requesting themoviedb API to get upcoming movies.", new Exception(erro));
                }

                var jsonResponse = JObject.Parse(responseContent);

                var paginatedMovieList = ParseJsonToPaginatedMovieList(jsonResponse);

                return paginatedMovieList;
            }
        }

        public async Task<PaginatedMovieList> SearchMovies(string query, int? page = null)
        {
            using (HttpClient http = new HttpClient())
            {
                var response = await http.GetAsync($"{API_URL}/search/movie?api_key={apiKey}&query={query}&page={page.GetValueOrDefault(1)}");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var erro = $"StatusCode:{response.StatusCode} Message: {response.ReasonPhrase} Response: {responseContent}";
                    throw new Exception("Error on requesting themoviedb API to search movies.", new Exception(erro));
                }

                var jsonResponse = JObject.Parse(responseContent);

                var paginatedMovieList = ParseJsonToPaginatedMovieList(jsonResponse);

                return paginatedMovieList;
            }
        }

        public async Task<Movie> GetMovieDetails(int id)
        {
            using (HttpClient http = new HttpClient())
            {
                var response = await http.GetAsync($"{API_URL}/movie/{id}?api_key={apiKey}");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var erro = $"StatusCode:{response.StatusCode} Message: {response.ReasonPhrase} Response: {responseContent}";
                    throw new Exception("Error on requesting themoviedb API to get movie details.", new Exception(erro));
                }

                var jsonResponse = JObject.Parse(responseContent);

                var movie = ParseJsonToMovie(jsonResponse);

                return movie;
            }
        }

        public async Task<IEnumerable<MovieGenre>> GetMovieGenres()
        {
            using (HttpClient http = new HttpClient())
            {
                var response = await http.GetAsync($"{API_URL}/genre/movie/list?api_key={apiKey}");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var erro = $"StatusCode:{response.StatusCode} Message: {response.ReasonPhrase} Response: {responseContent}";
                    throw new Exception("Error on requesting themoviedb API to get movie genres list.", new Exception(erro));
                }

                var jsonResponse = JObject.Parse(responseContent);

                var movieGenreList = ParseJsonToMovieGenreList(jsonResponse);

                return movieGenreList;
            }
        }

        private PaginatedMovieList ParseJsonToPaginatedMovieList(JObject jsonResponse)
        {
            var paginatedMovieList = new PaginatedMovieList();
            paginatedMovieList.Page = jsonResponse.Value<int>("page");
            paginatedMovieList.TotalPages = jsonResponse.Value<int>("total_pages");
            paginatedMovieList.TotalResults = jsonResponse.Value<int>("total_results");
            paginatedMovieList.Results = new List<Movie>();

            var results = jsonResponse.Value<JArray>("results");
            foreach (var jsonItem in results)
            {
                var movie = ParseJsonToMovie(jsonItem);

                paginatedMovieList.Results.Add(movie);
            }

            return paginatedMovieList;
        }

        private Movie ParseJsonToMovie(JToken jsonItem)
        {
            var movie = new Movie();
            movie.Id = jsonItem.Value<int>("id");
            movie.Title = jsonItem.Value<string>("title");
            movie.Overview = jsonItem.Value<string>("overview");
            movie.PosterPath = jsonItem.Value<string>("poster_path");
            movie.BackdropPath = jsonItem.Value<string>("backdrop_path");

            if (!string.IsNullOrEmpty(jsonItem.Value<string>("release_date")))
            {
                movie.ReleaseDate = jsonItem.Value<DateTime>("release_date");
            }
            
            if(jsonItem["genre_ids"] != null)
            {
                movie.GenreIds = jsonItem.Value<JArray>("genre_ids").Values<int>();
            }
            else if (jsonItem["genres"] != null)
            {
                //On movie details the genre has a different json structure
                movie.GenreIds = jsonItem.Value<JArray>("genres").Select(item => item.Value<int>("id"));
                movie.GenreNames = jsonItem.Value<JArray>("genres").Select(item => item.Value<string>("name"));
            }

            return movie;
        }

        private MovieGenre ParseJsonToMovieGenre(JToken jsonItem)
        {
            var movieGenre = new MovieGenre();
            movieGenre.Id = jsonItem.Value<int>("id");
            movieGenre.Name = jsonItem.Value<string>("name");

            return movieGenre;
        }

        private IEnumerable<MovieGenre> ParseJsonToMovieGenreList(JObject jsonResponse)
        {
            var movieGenreList = new List<MovieGenre>();

            var results = jsonResponse.Value<JArray>("genres");
            foreach (var jsonItem in results)
            {
                var movieGenre = ParseJsonToMovieGenre(jsonItem);

                movieGenreList.Add(movieGenre);
            }

            return movieGenreList;
        }
    }
}
