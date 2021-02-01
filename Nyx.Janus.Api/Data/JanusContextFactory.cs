using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Nyx.Janus.Api.Config;
using System;

namespace Nyx.Janus.Api.Data
{
    class JanusContextFactory : IDesignTimeDbContextFactory<JanusContext>
    {
        public JanusContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JanusContext>();
            var connectionString = GetConnectionString();

            optionsBuilder.UseSqlServer(connectionString,
                opt => opt.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds));
            return new JanusContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Obtain connection string information from the app settings in the API project.
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            var connectionStringsOptions = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .Build()
                .GetSection(ConnectionStringsOptions.ConnectionStrings)
                .Get<ConnectionStringsOptions>();
            var newConnectionString = connectionStringsOptions.SQL.Replace("Data Source=host.docker.internal,1433", "Server=.\\sqlexpress");
            Console.WriteLine($"SQL Connection String Established: {newConnectionString}");
            return newConnectionString;
        }
    }
}
