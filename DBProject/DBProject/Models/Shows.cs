using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DBProject.Models
{
    public class ShowLists
    {
        public List<Shows> allShows { set; get; }
        public List<Movies> allMovies { set; get; }
        public List<TVShows> allTVShows { set; get; }
    }

    public class Shows
    {
        public string showId { set; get; }
        public string showType { set; get; }
        public string movieId { set; get; }
        public string TVShowId { set; get; }
        public string AirDate { set; get; }
    }

    public class Movies
    {
        public string movieId { set; get; }
        public string movieName { set; get; }
        public string releaseDate { set; get; }
        public string movieRatings { set; get; }
        public string genreID { set; get; }
        public string genreName { set; get; }
    }

    public class TVShows
    {
        public string TVShowId { set; get; }
        public string TVShowName { set; get; }
        public string releaseDate { set; get; }
        public string TVShowRatings { set; get; }
        public string genreID { set; get; }
        public string genreName { set; get; }
    }

    public class Episode
    {
        public string returnDay { set; get; }
        public string episodeName { set; get; }
        public string episodeNumber { set; get; }
        public string seasonNumber { set; get; }
        public string seasonId { set; get; }
        public string TVShowName { set; get; }
    }
}
