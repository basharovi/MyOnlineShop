using Autofac;
using Microsoft.AspNetCore.Http;
using my_online_shop.Migrations.Data;

namespace my_online_shop
{
    public class WebModule : Module
    {
        private string _connectionString;
        private string _migrationAssemblyName;

        public WebModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostgresDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName);

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
