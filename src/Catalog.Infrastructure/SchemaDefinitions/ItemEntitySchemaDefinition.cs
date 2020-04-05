using System;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.SchemaDefinitions
{
	public class ItemEntitySchemaDefinition : IEntityTypeConfiguration<Item>
	{
		public void Configure(EntityTypeBuilder<Item> builder)
		{
            builder.ToTable("Items", CatalogContext.DEFAULT_SCHEMA);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .HasOne(p => p.Genre)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.GenreId);

            builder
                .HasOne(p => p.Artist)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.ArtistId);

            builder.Property(p => p.Price).HasConversion(
                p => $"{p.Amount}:{p.Currency}",
                p => new Price
                {
                    Amount = Convert.ToDecimal(
                        p.Split(":", StringSplitOptions.None)[0]),
                        Currency = p.Split(":", StringSplitOptions.None)[1]
                });

        }
	}
}
