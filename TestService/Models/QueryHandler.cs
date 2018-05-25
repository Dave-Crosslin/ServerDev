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

		private static MySqlConnection connection = new MySqlConnection();

		public string name;


		public void ConnectionOpen()
		{
			connection.ConnectionString = "Server=localhost;Database=TestDB;User ID=root;Password=DC;Pooling=false";
			connection.Open();
		}

		public void ConnectionClose()
		{
			connection.Close();
		}

		public string QueryCreate(string name)
		{
			string commtext = String.Format("SELECT Score, DT FROM Teams WHERE ID = '{0}' GROUP BY DT, Production;", name);
			return commtext;
		}

		public Dictionary<Int64, int> QueryExecute(string commtext)
		{
			Dictionary<Int64, int> Dict = new Dictionary<Int64, int>();

			MySqlCommand command = connection.CreateCommand();
			command.CommandText = commtext;
			using (MySqlDataReader Reader = command.ExecuteReader())
			{


				while (Reader.Read())
				{

					int myInt = Convert.ToInt32(Reader.GetValue(0).ToString());

					Int64 myDT = Int64.Parse(Convert.ToDateTime(Reader.GetValue(1).ToString()).ToString("MMDD"));

					Dict.Add(myDT, myInt);

				}

				return Dict;
			}
		}


		public List<string> spinnerFill(string commtext)
		{

			List<string> row = new List<string>();

			MySqlCommand command = connection.CreateCommand();
			command.CommandText = commtext;
			using (MySqlDataReader Reader = command.ExecuteReader())
			{

				while (Reader.Read())
				{

					for (int i = 0; i < Reader.FieldCount; i++)
					{
						row.Add(Reader.GetValue(i).ToString());
					}
				}
			}
			return row;
		}
	}
}
