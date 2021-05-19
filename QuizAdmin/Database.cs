using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuizAdmin
{
    public class Database
    {
        // connectionstring in this file now has value of connectionstring from another file AKA now being called conn, that is being passed through in form1.cs
        private string ConnectionString = "";
        public Database(string conn)
        {
            ConnectionString = conn;
        }

        // the connection is now being made to the database, you can now craft SQL queries to change the database (from this application)
        private MySqlConnection Connect()
        {
            var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            return connection;
        }

        public Database()
        {

        }
        public string[] QueryColToStringArray(string query, string collumn)
        {
            List<string> result= new List<string>();
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = query;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            result.Add(reader[collumn].ToString());
                        }
                    }
                }
            }
            return result.ToArray();
        }

        // basically, creates a dictionary (key:value pairs) code below will edit/turn data taken from database to something like this:
        //       var questions = new Dictionary<string, string>(){
        //{"quiz_id", "quiz_name"},
        //{"quiz_id", "quiz_name"},
        //{"quiz_id", "quiz_name"},
        //        };

        // now we can use the data in the application
        public Dictionary<string,string> QueryColsToDictionary(string query, string collumn1, string collumn2)
        {
            // creates dictionary
            Dictionary<string,string> result = new Dictionary<string, string>();
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = query;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // voeg collumn1 enzo toe in de dictionary
                            result.Add(reader[collumn1].ToString(), reader[collumn2].ToString());
                        }
                    }
                }
            }
            return result;
        }
        public List<Tuple<string, string, string,string>> Query4ColsToTuple(string query, string collumn1, string collumn2, string collumn3, string collum4)
        {
            List<Tuple<string, string, string,string>> result = new List<Tuple<string, string, string,string>>();
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = query;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Tuple<string,string,string,string>(reader[collumn1].ToString(), reader[collumn2].ToString(), reader[collumn3].ToString(), reader[collum4].ToString()));
                        }
                    }
                }
            }
            return result;
        }
        public string QueryColToString(string query, string collumn)
        {
            string result = "";
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = query;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result += reader[collumn].ToString() + Environment.NewLine;
                        }
                    }
                }
            }
            return result;
        }
        // multiple different SQL queries are getting executed here
        public void CustomQuery(string query)
        {
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = query;

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
