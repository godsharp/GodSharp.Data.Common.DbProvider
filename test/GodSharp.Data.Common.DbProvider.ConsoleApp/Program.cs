using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;

namespace GodSharp.Data.Common.DbProvider.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("db.provider.json");

            var config = builder.Build();

            DbProviderManager.LoadConfiguration(config);
            var conn = DbConnectionStringManager.ConnectionStrings["sqlite"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(conn.ProviderName);
            IDbConnection db = factory.CreateConnection();
            db.ConnectionString = conn.ConnectionString;
            db.Open();

            var cmd = db.CreateCommand();
            cmd.CommandText = "select datetime('now');";
            var dt = cmd.ExecuteScalar();
            System.Console.WriteLine(dt);

            db.Close();
        }
    }
}
