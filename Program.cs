using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsontTask3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MyConfiguration config = new MyConfiguration();
            var FilePath = config.GetFilePath();
            string connectionString = config.GetConnectionString();
            string LogPath = config.GetLogPath();

            Logger logger = new Logger(LogPath);

            /*JsonToSql jsonToSql = new JsonToSql(connectionString,logger);
            jsonToSql.ConvertJsonToSql(FilePath);*/

            SqlToJson jsonToSql = new SqlToJson(connectionString, logger);
            jsonToSql.ExportSqlToJson(FilePath);

        }
    }
}
