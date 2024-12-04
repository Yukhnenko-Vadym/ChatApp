using ChatApp.Data;
using ChatApp.Features.UserAuth.Models;
using ChatApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Repositories.Implementations;

public abstract class Repository<T>: IRepository<T> where T: class
{
    protected readonly ChatContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    protected Repository(ChatContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public abstract Task<T?> Get(Guid id);

    public async Task<T> Create(T obj)
    {
        await _dbSet.AddAsync(obj);
        
        await _dbContext.SaveChangesAsync();
        
        return obj;
    }

    public async Task<T> Update(T obj)
    {
        _dbSet.Update(obj);
        
        await _dbContext.SaveChangesAsync();

        return obj;
    }

    public async Task Delete(T obj)
    {
        _dbSet.Remove(obj);

        await _dbContext.SaveChangesAsync();
    }
}