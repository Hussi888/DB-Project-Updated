using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DBProject.Models
{
    public class Profile
    {
        public static Users DisplayInfo(string username)
        {
            Users User = new Users();
            SqlParameter paramBDate = new SqlParameter("@bdate", System.Data.SqlDbType.Date);
            SqlParameter paramGender = new SqlParameter("@gender", System.Data.SqlDbType.VarChar, 20);
            paramBDate.Direction = System.Data.ParameterDirection.Output;
            paramGender.Direction = System.Data.ParameterDirection.Output;
            using (SqlConnection con = new SqlConnection(CRUD.connectionString))
            {
                SqlCommand cmd = new SqlCommand("DisplayInfo", con);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add(paramBDate);
                cmd.Parameters.Add(paramGender);
                if (username != null)
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                User.username = username; 
                User.bDate = paramBDate.Value.ToString();
                var spaceIndex = User.bDate.IndexOf(" ");
                User.bDate = User.bDate.Remove(spaceIndex);
                User.gender = (string)paramGender.Value;
            }
            return User;
        }

        public static Shows[] WatchHistory(string username)
        {
            Shows[] shows = new Shows[10];

            return shows;
        }
    }
}
