using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Models
{
    public class ProfileModel
    {
        public Users user { set; get; }
        public List<Movies> allMovies { set; get; }
        public List<TVShows> allTVShows { set; get; }
    }

    public class Users
    {
        public string username { set; get; }
        public string password { set; get; }
        public string gender { set; get; }
        public string bDate { set; get; }
    }

}
