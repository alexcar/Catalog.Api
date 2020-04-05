using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Infrastructure.SchemaDefinitions;

namespace Catalog.Infrastructure
{
	public class CatalogContext : DbContext, IUnitOfWork
	{
		public const string DEFAULT_SCHEMA = "catalog";

		public CatalogContext(DbContextOptions<CatalogContext> options) :
			base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder) 
		{
			modelBuilder.ApplyConfiguration(new
				ItemEntitySchemaDefinition());
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new
				GenreEntitySchemaConfiguration());
			modelBuilder.ApplyConfiguration(new
				ArtistEntitySchemaConfiguration());

			base.OnModelCreating(modelBuilder);
		}

		public async Task<bool> SaveEntitiesAsync(CancellationToken 
			cancellationToken = default(CancellationToken))
		{
			await SaveChangesAsync(cancellationToken);
			return true;
		}

		public DbSet<Item> Items { get; set; }		
	}
}
