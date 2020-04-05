using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api.Extensions
{
	public static class DatabaseExtensions
	{
		public static IServiceCollection AddCatalogContext(this 
			IServiceCollection services, string connectionStrig)
		{
			return services
				.AddEntityFrameworkSqlServer()
				.AddDbContext<CatalogContext>(contextOptions =>
				{
					contextOptions.UseSqlServer(connectionStrig,
						serverOptions =>
						{
							serverOptions.MigrationsAssembly
								(typeof(Startup).Assembly.FullName);
						});
				});
		}
	}
}
