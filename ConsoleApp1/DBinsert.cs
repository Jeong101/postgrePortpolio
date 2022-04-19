using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.IO;
using System.Data.SqlClient;

namespace ConsoleApp1
{
	class DBinsert
	{
		//ファイルの読み込みの変数
		String line = "";
		//column名
		String[] colname = { "accesstime", "action", "treeid", "loginid",
			"client_ip", "server_ip", "client_mac", "server_mac", "smb1", "smb2",
			"size","sousa", "path" };
		
		//dbuserとconnection情報、pathを持っています。
		public void doinsert( DBuser dbo,String connstr,String path,String tbname)
		{	
			//set Connection
			var conn = new NpgsqlConnection( connstr );
			//errorが発生する場合、exception処理
			try
			{
				//NpgsqlCommand
				var cmd = new NpgsqlCommand();
				//connection open
				conn.Open();
				//db connect
				cmd.Connection = conn;

				DirectoryInfo di = new DirectoryInfo( path );

				//結果ファイルの数だけ振替
				foreach ( var fi in di.GetFiles( "Res*.log" ) )
				{
					//ファイルのLine数
					int lineCnt = 0;
					Console.WriteLine( fi.Name );

					//int txtLineCnt = 0;
					StreamReader file = new StreamReader( path + "\\" + fi.Name, Encoding.Default );
					//txtLineCnt = getLineCnt( file, path, fi.Name );

					while ( ( line = file.ReadLine() ) != null )
					{
						//値をもらう配列
						String[] col = new String[13];
						//Line数を上げる
						lineCnt++;
						
						Console.WriteLine( line );
						//最初のLineはcolumn名ですので2番名のLineからDBに入力
						if ( lineCnt > 1 )
						{
							//DBに入力する値を配列形式で取得
							 col = getValue( line );
							//sizeが0ではなくて""の場合、nullを入力する条件文
							if ( col[ 10 ].Equals( "" ) )
							{
								//set a sqlQuery by parameter value
								using ( var cm = new NpgsqlCommand( "insert into "+ tbname+" values(@accesstime, @action, @treeid, @loginid, @client_ip, @server_ip, @client_mac, @server_mac, @smb1, @smb2, @size, @sousa, @path)",conn ))
								{
									//set a each parameter's value
									for (int i=0;i<colname.Length;i++ )
									{
										//col[10]はsize(bigInt)ですのでparsingが必要
										if ( i == 10 ){
											cm.Parameters.AddWithValue( colname[i], DBNull.Value );
										}//if ( i == 10 ) END
										else
										//col[10]ない場合、文字でparameter set
										cm.Parameters.AddWithValue(colname[i],col[i]);
									}//for END
									//query実行
									cm.ExecuteNonQuery();
								}//using END
							}//if ( col[ 10 ].Equals( "" ) ) end

							//sizeが""がない場合
							else
							{
								//set a sqlQuery by parameter
								using ( var cm = new NpgsqlCommand( "insert into "+tbname+" values(@accesstime, @action, @treeid, @loginid, @client_ip, @server_ip, @client_mac, @server_mac, @smb1, @smb2, @size, @sousa, @path)", conn ) )
								{
									for ( int i = 0; i < colname.Length; i++ )
									{
										//col[10]はsize(bigInt)ですのでparsingが必要
										if ( i == 10 ){
											cm.Parameters.AddWithValue( colname[ i ], Int64.Parse(col[10]) );
										}//if ( i == 10 ) END
										else
											cm.Parameters.AddWithValue( colname[ i ], col[ i ] );
									}//for END
									cm.ExecuteNonQuery();
								}//using END
							}//else END							
						}//if ( lineCnt > 1 )END
					}//while ( ( line = file.ReadLine() ) != null ) END
					Console.WriteLine(fi.Name+ ":成功" );
				}//foreach ( var fi in di.GetFiles()) END
				conn.Close();
			}//try END
			catch ( Exception e ) {
				conn.Close();
				Console.WriteLine(e.Message);
			}//cath END
		}//doinsert END

		//値を取得メソッド
		String[] getValue(String line)
		{
			//DB入力する値を','でわけて配列に入れます
			String[] col = line.Split(',');
			//配列返還
			return col;
		}

		//ファイルのLine数を取得
		int getLineCnt( StreamReader file, String path, String name )
		{
			var lineCount = 0;
			using ( var reader = File.OpenText( path + "\\" + name ) )
			{
				while ( reader.ReadLine() != null )
				{
					lineCount++;
				}
			}
			//Line数返還
			return lineCount;
		}
	}
}
