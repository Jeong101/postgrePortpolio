using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
	class ReadFile
	{
		String[] pt = new string[ 2 ];
		//ファイルを読み込んで入れる変数
		string line;
		int topcnt = 0;
		public string[] writeFile()
		{
			Console.WriteLine( "pathを入力してください" );
			string path = Console.ReadLine();
			pt[ 0 ] = path;
			Console.WriteLine( "作るtable名を入力してください" );
			string tbn = Console.ReadLine();
			pt[ 1 ] = tbn;

			/*Encoding type:Shift-js
			 *code page:932
			 * utf-8,ANSIでする場合、
			 * 読み込んだ際、文字化けが発生。
			 *入力されたpathに結果ファイルを出力
			*/
			StreamWriter sw = new StreamWriter( File.Open( path + @"\Result.log", FileMode.Create ), Encoding.GetEncoding( 932 ) );

			//結果
			String topstr = "";			
			//Dircotoryにファイルがある場合
			if ( Directory.Exists( path ) )
			{				
				DirectoryInfo di = new DirectoryInfo( path );

				//logファイルの数だけ振替
				foreach ( var fi in di.GetFiles( "FAccLog*.log" ) )
				{	
					//ファイルLine数の変数
					int lineCnt = 0;
					Console.WriteLine( fi.Name );
					//logファイルの最後にある空間の処理変数
					int txtLineCnt = 0;
					StreamReader file = new StreamReader( path + "\\" + fi.Name, Encoding.Default );
					//読み込んだファイルのLine数
					txtLineCnt = getLineCnt( file, path, fi.Name );
					//ファイルのLineがないまで振替
					while ( ( line = file.ReadLine() ) != null )
					{
						//結果ファイルの一番上Lineを一回だけ書くため
						if ( topcnt == 0 )
						{
							topstr = line;
							sw.WriteLine(line);
							topcnt++;
						}//if ( topcnt == 0 ) END
						lineCnt++;
						Console.WriteLine( line );
						/*
						 * lineCnt > 1：最初のLineはcolumn名ですので抜きます。
						 * lineCnt!=txtLineCnt：最後のLineは空間が入れているので抜きます。
						*/
						if ( lineCnt > 1 && lineCnt!=txtLineCnt)
							{
								sw.WriteLine( line );
						}//if ( lineCnt > 1 && lineCnt!=txtLineCnt) END					
					}//while ( ( line = file.ReadLine() ) != null )END
				}//foreach ( var fi in di.GetFiles( "FAccLog*.log" ) ) END
			}//if ( Directory.Exists( path ) ) END
			sw.Close();
			return pt;
		}//writeFile() END

		//一つのlogファイルのLine数を数えるメソッド
		int getLineCnt(StreamReader file,String path,String name)
		{
			var lineCount = 0;
			using ( var reader = File.OpenText( path+"\\"+name ) )
			{
				while ( reader.ReadLine() != null )
				{
					lineCount++;
				}//while ( reader.ReadLine() != null ) END
			}//using ( var reader = File.OpenText( path+"\\"+name ) ) END
			return lineCount;
		}//getLineCnt END
	}
	
}
