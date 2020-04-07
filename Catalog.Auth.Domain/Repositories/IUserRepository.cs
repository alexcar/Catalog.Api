using Catalog.Auth.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Auth.Domain.Repositories
{
	public interface IUserRepository : IRepository
	{
		Task<bool> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
		Task<bool> SignUpAsync(User user, string password, CancellationToken cancellationToken = default);
		Task<User> GetByEmailAsync(string requestEmail, CancellationToken cancellationToken = default);
	}
}
