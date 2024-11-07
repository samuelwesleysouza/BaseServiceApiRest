using BaseServiceApiRest_Core.Transactions;
using BaseServiceRestApi_Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace BaseServiceRestApi_Infrastructure.Data.Transactions;

public class TransactionManager : ITransactionManager
{
    private BaseServiceRestApiContext _context;
    private IDbContextTransaction? _transaction;

    public TransactionManager(BaseServiceRestApiContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _transaction!.CommitAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }

    public async Task RollbackAsync()
    {
        await _transaction!.RollbackAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}