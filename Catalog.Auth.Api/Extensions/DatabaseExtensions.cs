using Catalog.Auth.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Auth.Api.Extensions
{
	public static class DatabaseExtensions
	{
		public static IServiceCollection AddAuthenticationContext(this
			IServiceCollection services, string connectionStrig)
		{
			return services
				.AddEntityFrameworkSqlServer()
				.AddDbContext<AuthenticationContext>(contextOptions =>
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
