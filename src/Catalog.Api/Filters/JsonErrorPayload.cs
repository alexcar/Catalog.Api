using System;

namespace Catalog.Api.Filters
{
	public class JsonErrorPayload
	{
		public int EventId { get; set; }
		public Object DetailedMessage { get; set; }
	}
}
