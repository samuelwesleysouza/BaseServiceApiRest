using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.Repositories.Base;
using BaseServiceRestApi_Infrastructure.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace BaseServiceRestApi_Infrastructure.Data.Repositories.Base;

public abstract class BaseSensitiveRepository<M> : ISensitiveRepository<M>
    where M : BaseEntity, IUserSensitive
{
    protected BaseServiceRestApiContext _context;
    protected long _currentUserId;
    protected bool _isAdmin;
    protected bool _isResale;
    protected bool _isAnonymous;
    public BaseSensitiveRepository(BaseServiceRestApiContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;

        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null) throw new ArgumentNullException();

        _isAnonymous = (bool)(httpContext.Items["isAnonymous"] ?? false);
        if (_isAnonymous) throw new HttpRequestException("Anonymous request attempted to sensitive resource", null, HttpStatusCode.Forbidden);


        var userIdClaim = httpContext.User.Claims.Where(x => x.Type == "id").FirstOrDefault();
        var userRole = httpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault();

        _isResale = userRole?.Value.ToLower() == "resale";

        _isAdmin = (bool)(httpContext.Items["isAdmin"] ?? false);

        if (_isAdmin is false)
            _currentUserId = long.Parse(userIdClaim?.Value!);
    }

    public virtual async Task<M?> Create(M model)
    {
        if (_isAdmin is false) model.UserId = _currentUserId;
        if (_isAdmin && model.UserId is 0) throw new HttpRequestException("UserId is required for SystemAdmin", null, HttpStatusCode.BadRequest);

        await _context.Set<M>().AddAsync(model);
        await _context.SaveChangesAsync();

        return model;
    }

    public virtual async Task<List<M>> GetAll()
    {
        var query = _context.Set<M>().AsQueryable();
        if (_isAdmin is false && _isResale is false)
            query = query.Where(x => x.UserId == _currentUserId);

        return await query.ToListAsync();
    }

    public virtual async Task<M?> GetById(long id, bool entityTracking = true)
    {
        var query = _context.Set<M>().AsQueryable();

        if (_isAdmin is false && _isResale is false)
            query = query.Where(x => x.UserId == _currentUserId);

        if (!entityTracking)
            query = query.AsNoTracking();

        return await query
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public virtual async Task HardDelete(M model)
    {
        _context.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<M?> SearchOne(Expression<Func<M, bool>> expression)
    {
        var query = _context.Set<M>().AsQueryable();

        if (_isAdmin is false)
            query = query.Where(x => x.UserId == _currentUserId);

        return await query
            .Where(expression)
            .FirstOrDefaultAsync();
    }

    public virtual async Task SoftDelete(M model)
    {
        model.IsDeleted = true;

        _context.Set<M>().Update(model);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<M?> Update(M model)
    {
        if (_isAdmin is false) model.UserId = _currentUserId;
        _context.Set<M>().Update(model);
        await _context.SaveChangesAsync();
        return model;
    }
}
