﻿using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
	public class ItemRepository : IItemRepository
	{
		private readonly CatalogContext _context;
		public IUnitOfWork UnitOfWork => _context;

		public ItemRepository(CatalogContext context)
		{
			_context = context ?? throw new 
				ArgumentNullException(nameof(context));
		}

		public async Task<IEnumerable<Item>> GetAsync()
		{
			return await _context
				.Items.Where(p => !p.IsInactive)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Item> GetAsync(Guid id)
		{
			var item = await _context.Items
				.AsNoTracking()
				.Where(p => p.Id == id)
				.Include(p => p.Genre)
				.Include(p => p.Artist).FirstOrDefaultAsync();

			return item;
		}		

		public Item Add(Item item)
		{
			return _context.Items.Add(item).Entity;
		}				

		public Item Update(Item item)
		{
			_context.Entry(item).State = EntityState.Modified;
			return item;
		}

		public async Task<IEnumerable<Item>> GetItemByArtistIdAsync(Guid id)
		{
			var items = await _context
				.Items
				.Where(p => !p.IsInactive)
				.Where(item => item.ArtistId == id)
				.Include(x => x.Genre)
				.Include(x => x.Artist)
				.ToListAsync();

			return items;
		}

		public async Task<IEnumerable<Item>> GetItemByGenreIdAsync(Guid id)
		{
			var items = await _context.Items
				.Where(p => !p.IsInactive)
				.Where(item => item.GenreId == id)
				.Include(x => x.Genre)
				.Include(x => x.Artist)
				.ToListAsync();

			return items;
		}
	}
}
