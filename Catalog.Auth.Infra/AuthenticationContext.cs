using Catalog.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Auth.Infra
{
	public class AuthenticationContext : IdentityDbContext<User>
	{
		public AuthenticationContext(DbContextOptions<AuthenticationContext> options)
			: base(options) 
		{

		}

		public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
		{
			await SaveChangesAsync(cancellationToken);
			return true;
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}

		public DbSet<User> Users { get; set; }
	}
}
