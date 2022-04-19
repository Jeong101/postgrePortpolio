using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class DBuser
	{
		public String host
		{get;set;
		}
		public String user
		{get;set;
		}
		public String pwd
		{get;set;
		}
		public String dbname
		{get;set;
		}
		

		public DBuser(String host,String user,String pwd,String dbname)
		{
			this.host = host;
			this.user = user;
			this.pwd = pwd;
			this.dbname = dbname;
		}
		
	}
}
