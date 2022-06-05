using Demo.Db.Repository;
using DEMOWebApi.Logger;
using Lamar;
using Serilog;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace DEMOWebApi.Lamar
{
    public class ApplicationRegistry : ServiceRegistry
    {
        public ApplicationRegistry()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
                scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly =>
                {
                    var name = assembly.GetName().Name;
                    return name != null && name.StartsWith("Demo.");
                });

                //   scanner.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
                

            });
            For(typeof(IRepository<>)).Use(typeof(Repository<>));
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program))?.Location);
            var logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .WriteTo.File(path + @"\Logs\log-{Date}.txt")
                .CreateLogger();
            Log.Logger = logger;
            For<ILogger>().Use(new ApplicationLogger(logger));


        }
    }
}
