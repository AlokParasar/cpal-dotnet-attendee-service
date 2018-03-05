using System;
using System.Data.Entity;
using VCapsParser;

namespace DataAccessLayer.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public sealed class SqlContext : DbContext
    {
        public SqlContext() : base("name=DefaultConnection") {
            string connectionString = VCapsEnvParser.GetConnectionString(ServiceType.MySql);

            //Console.Error.Write("ConnectionString:"+ connectionString);
            ////You can uncomment the below code to get the json, based on your attribute and then create the connection string from that.
            //if (string.IsNullOrEmpty(connectionString))
            //{
            //    var rawJson = VCapsEnvParser.GetRawData("yourattribute");
            //}
            
            if (string.IsNullOrEmpty(connectionString))
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlContext, DataAccessLayer.Migrations.Configuration>("DefaultConnection"));
            }
            else
            {
                Database.Connection.ConnectionString = connectionString;
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlContext, DataAccessLayer.Migrations.Configuration>());
            }

            //Console.Error.WriteLine("SetInitializer:");
        }
        public DbSet<AttendeeModel> ToDoItems { get; set; }
    }
}
