
namespace Catalog.Auth.Domain.Repositories
{
	public interface IRepository
	{
		IUnitOfWork UnitOfWork { get; }
	}
}
