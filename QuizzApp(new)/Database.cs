using System;

public class Class1
{
    public class Database
    {
        private string ConnectionString = "";
        public Database(string conn)
        {
            ConnectionString = conn;
        }
        public Database()
        {

        }
        public string[] QueryColToStringArray(string query, string collumn)
        {
            List<string> result = new List<string>();
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
                            result.Add(reader[collumn].ToString());
                        }
                    }
                }
            }
            return result.ToArray();
        }
        public Dictionary<string, string> QueryColsToDictionary(string query, string collumn1, string collumn2)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
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
                            result.Add(reader[collumn1].ToString(), reader[collumn2].ToString());
                        }
                    }
                }
            }
            return result;
        }
        public List<Tuple<string, string, string, string>> Query4ColsToDictionary(string query, string collumn1, string collumn2, string collumn3, string collum4)
        {
            List<Tuple<string, string, string, string>> result = new List<Tuple<string, string, string, string>>();
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
                            result.Add(new Tuple<string, string, string, string>(reader[collumn1].ToString(), reader[collumn2].ToString(), reader[collumn3].ToString(), reader[collum4].ToString()));
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
    }
}
