using Catalog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Catalog.Api.Filters
{
	public class ItemExistsAttribute : TypeFilterAttribute
	{
		public ItemExistsAttribute() : base(typeof(ItemExistsFilterImpl))
		{
		}

		public class ItemExistsFilterImpl : IAsyncActionFilter
		{
			private readonly IItemService _itemService;

			public ItemExistsFilterImpl(IItemService itemService)
			{
				_itemService = itemService;
			}
			public async Task OnActionExecutionAsync(
				ActionExecutingContext context, ActionExecutionDelegate next)
			{
				if (!(context.ActionArguments["id"] is Guid id))
				{
					context.Result = new BadRequestResult();
					return;
				}

				var result = await _itemService.GetItemAsync(
					new Domain.Requests.Item.GetItemRequest { Id = id });

				if (result == null)
				{
					context.Result = new NotFoundObjectResult(
						$"Item with id {id} not exist.");
					return;
				}

				await next();
			}
		}
	}
}
