using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
namespace TestService.Models
{
    public class QueryHandler
    {
        public QueryHandler()
        {
        }

        public string connectionString = "Server=localhost;Database=TestDB;User ID=root;Password=DC;Pooling=false";
		public string name;


		
		
		public string QueryCreate(string name)
		{
			string commtext = String.Format("SELECT Score, DT FROM Teams WHERE ID = '{0}' GROUP BY DT, Production;", name);
			return commtext;
		}

		public Dictionary<Int64, int> QueryExecute(string commtext)
		{
			Dictionary<Int64, int> Dict = new Dictionary<Int64, int>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(commtext)){    
                conn.Open();
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {


                    while (Reader.Read())
                    {

                        int myInt = Convert.ToInt32(Reader.GetValue(0).ToString());

                        Int64 myDT = Int64.Parse(Convert.ToDateTime(Reader.GetValue(1).ToString()).ToString("MMDD"));

                        Dict.Add(myDT, myInt);

                    }
                }
             }
			return Dict;
		}

            
        public List<string> spinnerFill()
        {
          

            List<string> row = new List<string>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand("SELECT ID FROM Teams;",conn)){

                conn.Open();
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {

                    while (Reader.Read())
                    {

                        for (int i = 0; i < Reader.FieldCount; i++)
                        {
                            if (row.Contains(Reader.GetValue(i).ToString()))
                            {
                                continue;
                            }
                            else
                            {
                                row.Add(Reader.GetValue(i).ToString());
                            }
                        }
                    }
                }
            }

         return row;
        }
	}
}
