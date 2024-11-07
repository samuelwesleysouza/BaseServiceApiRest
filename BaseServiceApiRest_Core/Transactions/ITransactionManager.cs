
namespace BaseServiceApiRest_Core.Transactions;

public interface ITransactionManager : IDisposable
{
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}