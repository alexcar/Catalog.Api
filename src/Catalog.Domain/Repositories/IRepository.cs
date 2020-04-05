
namespace Catalog.Domain.Repositories
{
	public interface IRepository
	{
		IUnitOfWork IUnitOfWork { get; }
	}
}
