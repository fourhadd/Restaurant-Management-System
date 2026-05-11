using FoodieRestaurant.Data;
using FoodieRestaurant.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodieRestaurant.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
    public void Delete(T entity) => DbSet.Remove(entity);
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) => await DbSet.Where(predicate).ToListAsync();
    public async Task<List<T>> GetAllAsync() => await DbSet.ToListAsync();
    public async Task<T?> GetByIdAsync(int id) => await DbSet.FindAsync(id);
    public async Task SaveAsync() => await Context.SaveChangesAsync();
    public void Update(T entity) => DbSet.Update(entity);
}
