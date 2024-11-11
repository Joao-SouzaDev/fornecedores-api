using fornecedor_api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace fornecedor_api.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly FornecedoresContext context;
    protected readonly DbSet<T> dbSet;

    public Repository(FornecedoresContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity != null)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}