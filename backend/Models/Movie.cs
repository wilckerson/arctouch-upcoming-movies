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
        public IEnumerable<string> GenreNames { get; set; }
        public string PosterPath { get; set; }

        /*
         * "popularity": 242.487,
            "vote_count": 134,
            "video": false,
            "poster_path": "/vqzNJRH4YyquRiWxCCOH0aXggHI.jpg",
            "id": 290859,
            "adult": false,
            "backdrop_path": "/a6cDxdwaQIFjSkXf7uskg78ZyTq.jpg",
            "original_language": "en",
            "original_title": "Terminator: Dark Fate",
            "genre_ids": [
                28,
                878
            ],
            "title": "Terminator: Dark Fate",
            "vote_average": 6.5,
            "overview": "More than two decades have passed since Sarah Connor prevented Judgment Day, changed the future, and re-wrote the fate of the human race. Dani Ramos is living a simple life in Mexico City with her brother and father when a highly advanced and deadly new Terminator – a Rev-9 – travels back through time to hunt and kill her. Dani's survival depends on her joining forces with two warriors: Grace, an enhanced super-soldier from the future, and a battle-hardened Sarah Connor. As the Rev-9 ruthlessly destroys everything and everyone in its path on the hunt for Dani, the three are led to a T-800 from Sarah’s past that may be their last best hope.",
            "release_date": "2019-11-01"
         */

    }
}
