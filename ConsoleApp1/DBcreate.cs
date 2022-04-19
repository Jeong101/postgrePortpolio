using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ConsoleApp1
{
	class DBcreate
	{
		public string doCreate( DBuser dbo, String connstr, String tbName)
		{
			//NpgsqlCommand
			var cmd = new NpgsqlCommand();
			//Connection
			var conn = new NpgsqlConnection( connstr );

			//connection open
			conn.Open();
			//db connect
			cmd.Connection = conn;
			//set sqlQuery
			cmd.CommandText = "create table public."+tbName+"(accesstime varchar(25),"
			+"action varchar(10),treeid varchar(10),loginid varchar(10),"
			+ "client_ip varchar(15),server_ip varchar(15),client_mac varchar(20),"
			+ "server_mac varchar(20),smb1 varchar(10),smb2 varchar(10),"
			+ "size bigint,sousa varchar(6),path varchar(500))";
			//Query実行
			cmd.ExecuteNonQuery();
			//close connection
			conn.Close();

			return tbName;
		}				
	}
}
