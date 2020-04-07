using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Auth.Domain.Entities
{
	public class User :IdentityUser
	{
		public string Name { get; set; }
	}
}
