using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DBProject.Models
{
    public class CRUD
    {
        public static string connectionString = "Data Source=HUSSAIN-PC\\SQLSVR;Initial Catalog=TVTDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static bool Signup(Users User)
        {
            bool check = false;
            SqlParameter param = new SqlParameter("@return", System.Data.SqlDbType.Int);
            param.Direction = System.Data.ParameterDirection.Output;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Signup", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = User.username;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 20).Value = User.password;
                cmd.Parameters.Add("@gender", System.Data.SqlDbType.VarChar, 10).Value = User.gender;
                cmd.Parameters.Add("@bdate", System.Data.SqlDbType.Date).Value = User.bDate;
                cmd.Parameters.Add(param);

                if (User.username != null && User.password != null && User.username != " ")
                {
                    check = true;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            if (!Convert.IsDBNull(param.Value) && check)
            {
                return true;
            }

            return false;
        }

        public static bool Login(Users User)
        {
            bool check = false;
            SqlParameter param = new SqlParameter("@return", System.Data.SqlDbType.Int);
            param.Direction = System.Data.ParameterDirection.Output;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Login", con);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = User.username;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 20).Value = User.password;
                cmd.Parameters.Add(param);
                if (User.username != null && User.password != null && User.username != " ")
                {
                    check = true;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            if (!Convert.IsDBNull(param.Value) && check)
            {
                return true;
            }

            return false;
        }

        public static List<Shows> getShows()
        {
            List<Shows> allShows = new List<Shows>();

            //Get Total number of shows in database
            SqlParameter param = new SqlParameter("@maxId", System.Data.SqlDbType.Int);
            param.Direction = System.Data.ParameterDirection.Output;
            using (SqlConnection con = new SqlConnection(CRUD.connectionString))
            {
                SqlCommand cmd = new SqlCommand("ShowCount", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //Create List for all shows
            for (int i = 1; i <= (int)param.Value; i++)
            {
                Shows s = new Shows();

                //Check if show exist for index
                SqlParameter returnParam = new SqlParameter("@return", System.Data.SqlDbType.Int);
                returnParam.Direction = System.Data.ParameterDirection.Output;
                using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("ShowExistByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;
                    cmd.Parameters.Add(returnParam);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                //Insert into list
                if (!Convert.IsDBNull(returnParam.Value))
                {
                    SqlParameter showTypeParam = new SqlParameter("@showType", System.Data.SqlDbType.VarChar, 10);
                    SqlParameter movieIDParam = new SqlParameter("@movieId", System.Data.SqlDbType.Int);
                    SqlParameter TVShowIDParam = new SqlParameter("@TVShowID", System.Data.SqlDbType.Int);
                    SqlParameter airDateParam = new SqlParameter("@airDate", System.Data.SqlDbType.Date);

                    showTypeParam.Direction = System.Data.ParameterDirection.Output;
                    movieIDParam.Direction = System.Data.ParameterDirection.Output;
                    TVShowIDParam.Direction = System.Data.ParameterDirection.Output;
                    airDateParam.Direction = System.Data.ParameterDirection.Output;

                    using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("GetShowByID", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;

                        cmd.Parameters.Add(showTypeParam);
                        cmd.Parameters.Add(movieIDParam);
                        cmd.Parameters.Add(TVShowIDParam);
                        cmd.Parameters.Add(airDateParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    s.showId = i.ToString();
                    s.showType = (string)showTypeParam.Value;
                    s.movieId = movieIDParam.Value.ToString();
                    s.TVShowId = TVShowIDParam.Value.ToString();
                    s.AirDate = airDateParam.Value.ToString();
                    var spaceIndex = s.AirDate.IndexOf(" ");
                    s.AirDate = s.AirDate.Remove(spaceIndex);
                    allShows.Add(s);
                }                
            }

            return allShows;
        }

        public static List<Movies> getMovies()
        {
            List<Movies> allMovies = new List<Movies>();

            //Get Total number of shows in database
            SqlParameter param = new SqlParameter("@maxId", System.Data.SqlDbType.Int);
            param.Direction = System.Data.ParameterDirection.Output;
            using (SqlConnection con = new SqlConnection(CRUD.connectionString))
            {
                SqlCommand cmd = new SqlCommand("ShowCountMovies", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //Create List for all shows
            for (int i = 1; i <= (int)param.Value; i++)
            {
                Movies m = new Movies();

                //Check if show exist for index
                SqlParameter returnParam = new SqlParameter("@return", System.Data.SqlDbType.Int);
                returnParam.Direction = System.Data.ParameterDirection.Output;
                using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("MovieExistByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;
                    cmd.Parameters.Add(returnParam);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                //Insert into list
                if (!Convert.IsDBNull(returnParam.Value))
                {
                    SqlParameter MovieNameParam = new SqlParameter("@MovieName", System.Data.SqlDbType.VarChar, 50);
                    SqlParameter releaseDateParam = new SqlParameter("@releaseDate", System.Data.SqlDbType.Date);
                    SqlParameter MovieRatingsParam = new SqlParameter("@MovieRatings", System.Data.SqlDbType.Int);
                    SqlParameter genreIDParam = new SqlParameter("@genreID", System.Data.SqlDbType.Int);

                    MovieNameParam.Direction = System.Data.ParameterDirection.Output;
                    releaseDateParam.Direction = System.Data.ParameterDirection.Output;
                    MovieRatingsParam.Direction = System.Data.ParameterDirection.Output;
                    genreIDParam.Direction = System.Data.ParameterDirection.Output;

                    using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("GetMovieByID", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;

                        cmd.Parameters.Add(MovieNameParam);
                        cmd.Parameters.Add(releaseDateParam);
                        cmd.Parameters.Add(MovieRatingsParam);
                        cmd.Parameters.Add(genreIDParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    m.movieId = i.ToString();
                    m.movieName = MovieNameParam.Value.ToString();
                    m.releaseDate = releaseDateParam.Value.ToString();
                    var spaceIndex = m.releaseDate.IndexOf(" ");
                    m.releaseDate = m.releaseDate.Remove(spaceIndex);
                    m.movieRatings = MovieRatingsParam.Value.ToString();
                    m.genreID = genreIDParam.Value.ToString();

                    //Get name of genre
                    SqlParameter genreNameParam = new SqlParameter("@genreName", System.Data.SqlDbType.VarChar, 20);
                    genreNameParam.Direction = System.Data.ParameterDirection.Output;
                    using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("GetGenreNameByID", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = Convert.ToInt32(m.genreID);
                        cmd.Parameters.Add(genreNameParam);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    m.genreName = genreNameParam.Value.ToString();
                    allMovies.Add(m);
                }
            }

            return allMovies;
        }

        public static List<TVShows> getTVShows()
        {
            List<TVShows> allTVShows = new List<TVShows>();

            //Get Total number of shows in database
            SqlParameter param = new SqlParameter("@maxId", System.Data.SqlDbType.Int);
            param.Direction = System.Data.ParameterDirection.Output;
            using (SqlConnection con = new SqlConnection(CRUD.connectionString))
            {
                SqlCommand cmd = new SqlCommand("ShowCountTVShows", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //Create List for all shows
            for (int i = 1; i <= (int)param.Value; i++)
            {
                TVShows t = new TVShows();

                //Check if show exist for index
                SqlParameter returnParam = new SqlParameter("@return", System.Data.SqlDbType.Int);
                returnParam.Direction = System.Data.ParameterDirection.Output;
                using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("TVShowExistByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;
                    cmd.Parameters.Add(returnParam);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                //Insert into list
                if (!Convert.IsDBNull(returnParam.Value))
                {
                    SqlParameter TVShowNameParam = new SqlParameter("@TVShowName", System.Data.SqlDbType.VarChar, 50);
                    SqlParameter releaseDateParam = new SqlParameter("@releaseDate", System.Data.SqlDbType.Date);
                    SqlParameter TVShowRatingsParam = new SqlParameter("@TVShowRatings", System.Data.SqlDbType.Int);
                    SqlParameter genreIDParam = new SqlParameter("@genreID", System.Data.SqlDbType.Int);

                    TVShowNameParam.Direction = System.Data.ParameterDirection.Output;
                    releaseDateParam.Direction = System.Data.ParameterDirection.Output;
                    TVShowRatingsParam.Direction = System.Data.ParameterDirection.Output;
                    genreIDParam.Direction = System.Data.ParameterDirection.Output;

                    using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("GetTVShowByID", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;

                        cmd.Parameters.Add(TVShowNameParam);
                        cmd.Parameters.Add(releaseDateParam);
                        cmd.Parameters.Add(TVShowRatingsParam);
                        cmd.Parameters.Add(genreIDParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    t.TVShowId= i.ToString();
                    t.TVShowName = TVShowNameParam.Value.ToString();
                    t.releaseDate = releaseDateParam.Value.ToString();
                    var spaceIndex = t.releaseDate.IndexOf(" ");
                    t.releaseDate = t.releaseDate.Remove(spaceIndex);
                    t.TVShowRatings= TVShowRatingsParam.Value.ToString();
                    t.genreID = genreIDParam.Value.ToString();

                    //Get name of genre
                    SqlParameter genreNameParam = new SqlParameter("@genreName", System.Data.SqlDbType.VarChar, 20);
                    genreNameParam.Direction = System.Data.ParameterDirection.Output;
                    using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("GetGenreNameByID", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = Convert.ToInt32(t.genreID);
                        cmd.Parameters.Add(genreNameParam);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    t.genreName = genreNameParam.Value.ToString();

                    allTVShows.Add(t);
                }
            }

            return allTVShows;
        }

        public static List<Movies> GetTrendingMovies()
        {
            List<Movies> allMovies = new List<Movies>();

            //Get Total number of shows in database
            SqlParameter param = new SqlParameter("@maxId", System.Data.SqlDbType.Int);
            param.Direction = System.Data.ParameterDirection.Output;
            using (SqlConnection con = new SqlConnection(CRUD.connectionString))
            {
                SqlCommand cmd = new SqlCommand("ShowCountMovies", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //Create List for all shows
            for (int i = 1; i <= (int)param.Value; i++)
            {
                Movies m = new Movies();

                //Check if show exist for index
                SqlParameter returnParam = new SqlParameter("@return", System.Data.SqlDbType.Int);
                returnParam.Direction = System.Data.ParameterDirection.Output;
                using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("MovieExistByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;
                    cmd.Parameters.Add(returnParam);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                //Insert into list
                if (!Convert.IsDBNull(returnParam.Value))
                {
                    //Check if trending
                    SqlParameter returnTrend = new SqlParameter("@return", System.Data.SqlDbType.Int);
                    returnTrend.Direction = System.Data.ParameterDirection.Output;
                    using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("CheckTrendingMovies", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;
                        cmd.Parameters.Add(returnTrend);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    if (!Convert.IsDBNull(returnTrend.Value))
                    {
                        SqlParameter MovieNameParam = new SqlParameter("@MovieName", System.Data.SqlDbType.VarChar, 50);
                        SqlParameter releaseDateParam = new SqlParameter("@releaseDate", System.Data.SqlDbType.Date);
                        SqlParameter MovieRatingsParam = new SqlParameter("@MovieRatings", System.Data.SqlDbType.Int);
                        SqlParameter genreIDParam = new SqlParameter("@genreID", System.Data.SqlDbType.Int);

                        MovieNameParam.Direction = System.Data.ParameterDirection.Output;
                        releaseDateParam.Direction = System.Data.ParameterDirection.Output;
                        MovieRatingsParam.Direction = System.Data.ParameterDirection.Output;
                        genreIDParam.Direction = System.Data.ParameterDirection.Output;

                        using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("GetMovieByID", con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;

                            cmd.Parameters.Add(MovieNameParam);
                            cmd.Parameters.Add(releaseDateParam);
                            cmd.Parameters.Add(MovieRatingsParam);
                            cmd.Parameters.Add(genreIDParam);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        m.movieId = i.ToString();
                        m.movieName = MovieNameParam.Value.ToString();
                        m.releaseDate = releaseDateParam.Value.ToString();
                        var spaceIndex = m.releaseDate.IndexOf(" ");
                        m.releaseDate = m.releaseDate.Remove(spaceIndex);
                        m.movieRatings = MovieRatingsParam.Value.ToString();
                        m.genreID = genreIDParam.Value.ToString();

                        //Get name of genre
                        SqlParameter genreNameParam = new SqlParameter("@genreName", System.Data.SqlDbType.VarChar, 20);
                        genreNameParam.Direction = System.Data.ParameterDirection.Output;
                        using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("GetGenreNameByID", con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = Convert.ToInt32(m.genreID);
                            cmd.Parameters.Add(genreNameParam);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        m.genreName = genreNameParam.Value.ToString();

                        allMovies.Add(m);
                    }

                }
            }

            return allMovies;
        }

        public static List<TVShows> GetTrendingTVShows()
        {
            List<TVShows> allTVShows = new List<TVShows>();

            //Get Total number of shows in database
            SqlParameter param = new SqlParameter("@maxId", System.Data.SqlDbType.Int);
            param.Direction = System.Data.ParameterDirection.Output;
            using (SqlConnection con = new SqlConnection(CRUD.connectionString))
            {
                SqlCommand cmd = new SqlCommand("ShowCountTVShows", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //Create List for all shows
            for (int i = 1; i <= (int)param.Value; i++)
            {
                TVShows t = new TVShows();

                //Check if show exist for index
                SqlParameter returnParam = new SqlParameter("@return", System.Data.SqlDbType.Int);
                returnParam.Direction = System.Data.ParameterDirection.Output;
                using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("TVShowExistByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;
                    cmd.Parameters.Add(returnParam);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                //Insert into list
                if (!Convert.IsDBNull(returnParam.Value))
                {
                    //Check if trending
                    SqlParameter returnTrend = new SqlParameter("@return", System.Data.SqlDbType.Int);
                    returnTrend.Direction = System.Data.ParameterDirection.Output;
                    using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("CheckTrendingTVShows", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;
                        cmd.Parameters.Add(returnTrend);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    if (!Convert.IsDBNull(returnTrend.Value))
                    {
                        SqlParameter TVShowNameParam = new SqlParameter("@TVShowName", System.Data.SqlDbType.VarChar, 50);
                        SqlParameter releaseDateParam = new SqlParameter("@releaseDate", System.Data.SqlDbType.Date);
                        SqlParameter TVShowRatingsParam = new SqlParameter("@TVShowRatings", System.Data.SqlDbType.Int);
                        SqlParameter genreIDParam = new SqlParameter("@genreID", System.Data.SqlDbType.Int);

                        TVShowNameParam.Direction = System.Data.ParameterDirection.Output;
                        releaseDateParam.Direction = System.Data.ParameterDirection.Output;
                        TVShowRatingsParam.Direction = System.Data.ParameterDirection.Output;
                        genreIDParam.Direction = System.Data.ParameterDirection.Output;

                        using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("GetTVShowByID", con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;

                            cmd.Parameters.Add(TVShowNameParam);
                            cmd.Parameters.Add(releaseDateParam);
                            cmd.Parameters.Add(TVShowRatingsParam);
                            cmd.Parameters.Add(genreIDParam);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        t.TVShowId = i.ToString();
                        t.TVShowName = TVShowNameParam.Value.ToString();
                        t.releaseDate = releaseDateParam.Value.ToString();
                        var spaceIndex = t.releaseDate.IndexOf(" ");
                        t.releaseDate = t.releaseDate.Remove(spaceIndex);
                        t.TVShowRatings = TVShowRatingsParam.Value.ToString();
                        t.genreID = genreIDParam.Value.ToString();

                        //Get name of genre
                        SqlParameter genreNameParam = new SqlParameter("@genreName", System.Data.SqlDbType.VarChar, 20);
                        genreNameParam.Direction = System.Data.ParameterDirection.Output;
                        using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("GetGenreNameByID", con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = Convert.ToInt32(t.genreID);
                            cmd.Parameters.Add(genreNameParam);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        t.genreName = genreNameParam.Value.ToString();

                        allTVShows.Add(t);
                    }

                }
            }

            return allTVShows;
        }

        public static List<Episode> GetUpcomingEpisodes()
        {
            List<Episode> allCurrentEpisodes = new List<Episode>();

            //Get Total number of epsides in currList of episodes
            SqlParameter param = new SqlParameter("@maxId", System.Data.SqlDbType.Int);
            param.Direction = System.Data.ParameterDirection.Output;
            using (SqlConnection con = new SqlConnection(CRUD.connectionString))
            {
                SqlCommand cmd = new SqlCommand("MaxIDEpisodeInCurr", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //Create List for all relevant episodes
            for (int i = 1; i <= (int)param.Value; i++)
            {
                Episode e = new Episode();

                //Check if show exist for index
                SqlParameter returnParam = new SqlParameter("@return", System.Data.SqlDbType.Int);
                returnParam.Direction = System.Data.ParameterDirection.Output;
                using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("EpisodeExistByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;
                    cmd.Parameters.Add(returnParam);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                //Insert into list
                if (!Convert.IsDBNull(returnParam.Value))
                {
                    SqlParameter returnDayParam = new SqlParameter("@returnDay", System.Data.SqlDbType.Int);
                    SqlParameter episodeNameParam = new SqlParameter("@episodeName", System.Data.SqlDbType.VarChar, 50);
                    SqlParameter episodeNumberParam = new SqlParameter("@episodeNumber", System.Data.SqlDbType.Int);
                    SqlParameter seasonNumberParam = new SqlParameter("@seasonNumber", System.Data.SqlDbType.Int);
                    SqlParameter seasonID = new SqlParameter("@seasonID", System.Data.SqlDbType.Int);

                    returnDayParam.Direction = System.Data.ParameterDirection.Output;
                    episodeNameParam.Direction = System.Data.ParameterDirection.Output;
                    episodeNumberParam.Direction = System.Data.ParameterDirection.Output;
                    seasonNumberParam.Direction = System.Data.ParameterDirection.Output;
                    seasonID.Direction = System.Data.ParameterDirection.Output;

                    using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("EpisodeCurrByID", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idInput", System.Data.SqlDbType.Int).Value = i;

                        cmd.Parameters.Add(returnDayParam);
                        cmd.Parameters.Add(episodeNameParam);
                        cmd.Parameters.Add(episodeNumberParam);
                        cmd.Parameters.Add(seasonNumberParam);
                        cmd.Parameters.Add(seasonID);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    e.returnDay = returnDayParam.Value.ToString();
                    e.episodeName = episodeNameParam.Value.ToString();
                    e.episodeNumber = episodeNumberParam.Value.ToString();
                    e.seasonNumber = seasonNumberParam.Value.ToString();
                    e.seasonId = seasonID.Value.ToString();

                    SqlParameter TVShowNameParam = new SqlParameter("@TVShowName", System.Data.SqlDbType.VarChar, 50);

                    TVShowNameParam.Direction = System.Data.ParameterDirection.Output;

                    using (SqlConnection con = new SqlConnection(CRUD.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("TVShowFromSeason", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@seasonID", System.Data.SqlDbType.Int).Value = Convert.ToInt32(e.seasonId);
        
                        cmd.Parameters.Add(TVShowNameParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    e.TVShowName = TVShowNameParam.Value.ToString(); 

                    allCurrentEpisodes.Add(e);

                }
            }

            return allCurrentEpisodes;
        }
    }

}