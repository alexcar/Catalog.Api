﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Responses.Item
{
	public class PriceResponse
	{
		public decimal Amount { get; set; }
		public string Currency { get; set; }
	}
}
