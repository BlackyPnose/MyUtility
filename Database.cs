using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//I install the NuGet Package for MySql. Then use it
using MySql.Data.MySqlClient;


namespace MyUtility
{
    class Database
    {
        //I set and rename my connection with the database
        private MySqlConnection con;

        //I set all the needed information to access my server
        public Database(string nameDB, string server = "localhost", string user = "root", string pass = "rooot")
        {
            con = new MySqlConnection($"SERVER={server};DATABASE={nameDB};UID={user};Password={pass};");
        }

        public List<Dictionary<string, string>> Read(string query)
        {
            List<Dictionary<string, string>> ris = new List<Dictionary<string, string>>();

            con.Open();

            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Dictionary<string, string> line = new Dictionary<string, string>();

                for (int i = 0; i < dr.FieldCount; i++)
                    line.Add(dr.GetName(i).ToLower(), dr.GetValue(i).ToString());

                ris.Add(line);
            }

            dr.Close();
            con.Close();

            return ris;
        }

        public bool Update(string query)
        {
            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"Error during the update() from query:\n{query.ToUpper()}");
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public Dictionary<string, string> ReadOne(string query)
        {
            try
            {
                return Read(query)[0];
            }
            catch
            {
                return null;
            }
        }
    }
}
