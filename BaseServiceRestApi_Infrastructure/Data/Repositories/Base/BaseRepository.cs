using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using BaseServiceApiRest_Core.Interfaces.Repositories.Base;
using BaseServiceApiRest_Core.Entities;
using BaseServiceRestApi_Infrastructure.Data.Context;
using BaseServiceApiRest_Core.Transactions;
using Microsoft.Data.SqlClient;

namespace BaseServiceRestApi_Infrastructure.Data.Repositories.Base;

public abstract class BaseRepository<M> : IRepository<M> where M : BaseEntity
{
    protected BaseServiceRestApiContext _context;
    protected ITransactionManager _transaction;

    public BaseRepository(BaseServiceRestApiContext context, ITransactionManager transactionManager)
    {
        _context = context;
        _transaction = transactionManager;
    }

    public virtual async Task<M?> Create(M model)
    {
        try
        {
            // Inicia a transação
            await _transaction.BeginTransactionAsync();

            // Define o ID como 0, assumindo que o banco de dados irá gerá-lo automaticamente
            model.Id = 0;

            // Adiciona o modelo ao contexto do banco de dados
            await _context.Set<M>().AddAsync(model);

            // Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();

            // Confirma a transação
            await _transaction.CommitAsync();

            return model; // Retorna o modelo com o ID gerado pelo banco de dados
        }
        catch (DbUpdateException ex)
        {
            // Faz o rollback da transação em caso de erro
            await _transaction.RollbackAsync();

            // Verifica se a exceção foi causada por um erro de violação de chave estrangeira ou índice único
            if (ex.InnerException is SqlException sqlEx)
            {
                // Código de erro SQL Server para violação de chave estrangeira (FK constraint)
                if (sqlEx.Number == 547) // 547 é o código de erro para violação de chave estrangeira no SQL Server
                {
                    throw new HttpRequestException($"Foreign Key constraint violated: {sqlEx.Message}", ex, HttpStatusCode.Conflict);
                }

                // Código de erro SQL Server para violação de índice único (Unique constraint)
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // 2627 e 2601 são os códigos de erro para violação de índice único
                {
                    throw new HttpRequestException($"Unique index violated: {sqlEx.Message}", ex, HttpStatusCode.Conflict);
                }
            }

            // Re-throw a exceção se não for um erro esperado
            throw;
        }
    }


    public virtual async Task<List<M>> GetAll()
    {
        return await _context.Set<M>().ToListAsync();
    }

    public virtual async Task<M?> GetById(long id, bool entityTracking = true)
    {
        var query = _context.Set<M>().AsQueryable();

        if (!entityTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == id);

    }

    public async Task<M?> SearchOne(Expression<Func<M, bool>> expression)
    {
        return await _context.Set<M>()
            .Where(expression)
            .FirstOrDefaultAsync();
    }

    public virtual async Task<M?> Update(M model)
    {
        try
        {
            await _transaction.BeginTransactionAsync();

            _context.Set<M>().Update(model);
            await _context.SaveChangesAsync();

            await _transaction.CommitAsync();

            return model;
        }
        catch (Exception)
        {
            await _transaction.RollbackAsync();
            throw;
        }

    }

    public virtual async Task SoftDelete(M model)
    {
        try
        {
            await _transaction.BeginTransactionAsync();

            model.IsDeleted = true;
            _context.Set<M>().Update(model);
            await _context.SaveChangesAsync();

            await _transaction.CommitAsync();
        }
        catch (Exception)
        {
            await _transaction.RollbackAsync();
            throw;
        }
    }

    public virtual async Task HardDelete(M model)
    {
        try
        {
            await _transaction.BeginTransactionAsync();

            _context.Set<M>().Remove(model);
            await _context.SaveChangesAsync();

            await _transaction.CommitAsync();
        }
        catch (Exception)
        {
            await _transaction.RollbackAsync();
            throw;
        }
    }
}