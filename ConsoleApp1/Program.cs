using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
	class Program
	{
		static void Main( string[] args )
		{
			//logファイルを整理するclass
			ReadFile rf = new ReadFile();			

			//dbに接続するclass
			DBconnection conn = new DBconnection();

			/*
			 * rf.writeFileはlogファイルを一つのファイルで作成してpathを返還
			 */
			conn.connection( rf.writeFile() );//db接続、table作成、insert query実行
			
		}
	}
}