using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTouch.UpcomingMovies.Api.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IEnumerable<int> GenreIds { get; set; }        
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }

        public int? Runtime { get; set; }       
        public float? UserScore { get; set; }
        public IEnumerable<Cast> CastList { get; set; }
        public string TrailerUrl { get; set; }
        
    }
}
