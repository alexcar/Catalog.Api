﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Entities;
using Catalog.Fixtures;
using Catalog.Infrastructure.Repositories;
using Shouldly;
using Xunit;

namespace Catalog.Infrastructure.Tests
{
	public class ItemRepositoryTests : IClassFixture<CatalogContextFactory>
	{
		private readonly ItemRepository _sut;
		private readonly TestCatalogContext _context;
		
		public ItemRepositoryTests(CatalogContextFactory catalogContextFactory)
		{
			_context = catalogContextFactory.ContextInstance;
			_sut = new ItemRepository(_context);
		}
		
		[Fact]
		public async Task should_get_data()
		{
			//var options = new DbContextOptionsBuilder<CatalogContext>()
			//	.UseInMemoryDatabase(databaseName: "should_get_data")
			//	.Options;

			//await using var context = new TestCatalogContext(options);
			//context.Database.EnsureCreated();
			//var sut = new ItemRepository(context);
			//var result = await sut.GetAsync();

			//result.ShouldNotBeNull();
			var result = await _sut.GetAsync();
			result.ShouldNotBeNull();
		}

		[Fact]
		public async Task should_returns_null_with_id_not_present()
		{
			//var options = new DbContextOptionsBuilder<CatalogContext>()
			//.UseInMemoryDatabase(databaseName:
			//"should_returns_null_with_id_not_present")
			//.Options;

			//await using var context = new TestCatalogContext(options);
			//context.Database.EnsureCreated();

			//var sut = new ItemRepository(context);
			//var result = await sut.GetAsync(Guid.NewGuid());
			//result.ShouldBeNull();

			var result = await _sut.GetAsync(Guid.NewGuid());
			result.ShouldBeNull();
		}

		[Theory]
		[InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
		public async Task should_return_record_by_id(string guid)
		{
			//var options = new DbContextOptionsBuilder<CatalogContext>()
			//.UseInMemoryDatabase(databaseName:
			//"should_return_record_by_id")
			//.Options;

			//await using var context = new TestCatalogContext(options);
			//context.Database.EnsureCreated();

			//var sut = new ItemRepository(context);
			//var result = await sut.GetAsync(new Guid(guid));
			//result.Id.ShouldBe(new Guid(guid));

			var result = await _sut.GetAsync(new Guid(guid));
			result.Id.ShouldBe(new Guid(guid));
		}

		[Fact]
		public async Task should_add_new_item()
		{
			var testItem = new Item
			{
				Name = "Test album",
				Description = "Description",
				LabelName = "Label name",
				Price = new Price { Amount = 13, Currency = "EUR" },
				PictureUri = "https://mycdn.com/pictures/32423423",
				ReleaseDate = DateTimeOffset.Now,
				AvailableStock = 6,
				GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
				ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
			};

			//var options = new DbContextOptionsBuilder<CatalogContext>()
			//.UseInMemoryDatabase("should_add_new_items")
			//.Options;

			//await using var context = new TestCatalogContext(options);
			//context.Database.EnsureCreated();

			//var sut = new ItemRepository(context);
			//sut.Add(testItem);
			//await sut.IUnitOfWork.SaveEntitiesAsync();

			_sut.Add(testItem);
			await _sut.UnitOfWork.SaveEntitiesAsync();

			_context.Items
				.FirstOrDefault(_ => _.Id == testItem.Id)
				.ShouldNotBeNull();
		}
		[Fact]
		public async Task should_update_item()
		{
			var testItem = new Item
			{
				Id = new Guid("b5b05534-9263-448c-a69e-0bbd8b3eb90e"),
				Name = "Test album",
				Description = "Description updated",
				LabelName = "Label name",
				Price = new Price { Amount = 50, Currency = "EUR" },
				PictureUri = "https://mycdn.com/pictures/32423423",
				ReleaseDate = DateTimeOffset.Now,
				AvailableStock = 6,
				GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
				ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
			};

			//var options = new DbContextOptionsBuilder<CatalogContext>()
			//.UseInMemoryDatabase("should_update_item")
			//.Options;

			//await using var context = new TestCatalogContext(options);
			//context.Database.EnsureCreated();

			//var sut = new ItemRepository(context);
			//sut.Update(testItem);
			//await sut.IUnitOfWork.SaveEntitiesAsync();

			//context.Items
			//.FirstOrDefault(x => x.Id == testItem.Id)
			//?.Description.ShouldBe("Description updated");

			_sut.Update(testItem);

			await _sut.UnitOfWork.SaveEntitiesAsync();

			var result = _context.Items
				.FirstOrDefault(item => item.Id == testItem.Id);

			result.Description.ShouldBe("Description updated");
			result.Price.Amount.ShouldBe(50);
		}
	}
}
