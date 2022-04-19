using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ConsoleApp1
{
	class DBconnection
	{		
		public void connection(String[] pt)
		{
			String path = pt[ 0 ];
			//db user
			DBuser dbo = getInfo();

			//table create
			DBcreate dbc = new DBcreate();

			//insert
			DBinsert dbi = new DBinsert();
					
			String connstr = "Host=" + dbo.host + ";Username=" + dbo.user + ";Password="+dbo.pwd+";Database="+dbo.dbname+";";
			
			//database connection
			var conn = new NpgsqlConnection( connstr );
			
			try {				
				//table作成
				string tbname = dbc.doCreate(dbo,connstr,pt[1]);				
				
				//db入力
				dbi.doinsert( dbo,connstr, path,tbname);	
			}//try END
			catch ( Exception ex ) {
				Console.WriteLine( "============== Error ==============" );				
				Console.WriteLine( ex.Message );				
			}//catch END
			Console.ReadLine();
		}// connection() END		

		public DBuser getInfo()
		{
			Console.WriteLine( "Host名を入力してください" );
			//String host = Console.ReadLine();
			String host = "fz999s0105";

			Console.WriteLine( "UserNameを入力してください" );
			//String user = Console.ReadLine();
			String user = "postgres";

			Console.WriteLine( "Passwordを入力してください" );
			//String pwd = Console.ReadLine();
			String pwd = "ExcelCreates";

			Console.WriteLine( "Database名を入力してください" );
			//String dbname = Console.ReadLine();
			String dbname = "forz2db";

			DBuser dbo = new DBuser( host, user, pwd, dbname );

			return dbo;
		}

	}//class END
}