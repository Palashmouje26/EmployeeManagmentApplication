using EmployeeManagmentApplication.Modal.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagmentApplicationRepository.Data
{
    public class DataRepository : IDataRepository
    {
        #region Private Member
        private readonly EmployeeDBContext _DBContext;
        #endregion

        #region Constructor
        public DataRepository(EmployeeDBContext dbContext)
        {
            _DBContext = dbContext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds entity to the DbSet.
        /// </summary>
        /// <typeparam name="T">Model class to create DbSet.</typeparam>
        /// <param name="entity">Entity to add.</param>
        /// <returns>Task</returns>
        public async Task AddAsync<T>(T entity) where T : class
        {
            var dbSet = CreateDbSetAsync<T>();
            await dbSet.AddAsync(entity);
            await _DBContext.SaveChangesAsync();
        }

        public Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }
        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get first row from respective DbSet.
        /// </summary>
        /// <typeparam name="T">Model class to create DbSet.</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Generic Entity</returns>
        public async Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var dbSet = CreateDbSetAsync<T>();
            return await dbSet.FirstAsync(predicate);
        }

        /// <summary>
        /// Get first row from respective DbSet if data not found then return null.
        /// </summary>
        /// <typeparam name="T">Model class to create DbSet.</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>If row found then return enitity else return null</returns>
        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var dbSet = CreateDbSetAsync<T>();
            return dbSet.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Retrieves all the data.
        /// </summary>
        /// <typeparam name="T">Model class to create DbSet.</typeparam>
        /// <returns>List of all the elements.</returns>
        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            var dbSet = CreateDbSetAsync<T>();
            var values = await dbSet.ToListAsync();
            return values;
        }

        public IDbContextTransaction GetTransaction()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Remove entity from the DbSet.
        /// </summary>
        /// <typeparam name="T">Model class to create DbSet.</typeparam>
        /// <param name="entity">Entity to remove.</param>
        /// <returns>Task</returns>
        public async Task RemoveAsync<T>(T entity) where T : class
        {
            var dbSet = CreateDbSetAsync<T>();
            dbSet.Remove(entity);
            await _DBContext.SaveChangesAsync();
        }

        public Task RemoveRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tresult> Select<T, Tresult>(Expression<Func<T, Tresult>> predicate) where T : class
        {
            throw new NotImplementedException();
        }

        public void TransactionBegin()
        {
            throw new NotImplementedException();
        }

        public void TransactionCommit()
        {
            throw new NotImplementedException();
        }

        public void TransactionRollback()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates entity in the DbSet.
        /// </summary>
        /// <typeparam name="T">Model class to create DbSet.</typeparam>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Task</returns>
        public async Task UpdateAsync<T>(T entity) where T : class
        {
            var dbSet = CreateDbSetAsync<T>();
            dbSet.Update(entity);
            await _DBContext.SaveChangesAsync();
        }

        public Task UpdateRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Filters data based on predicate.
        /// </summary>
        /// <typeparam name="T">Model class to create DbSet.</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>IQueryable containing filtered elements.</returns>
        public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var dbSet = CreateDbSetAsync<T>();

            return dbSet.Where(predicate);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Creates DbSet.
        /// </summary>
        /// <typeparam name="T">Model class to create DbSet.</typeparam>
        /// Model class for creating set.
        /// </typeparam>
        /// <returns>A DbSet object.</returns>
        private DbSet<T> CreateDbSetAsync<T>()
        where T : class
        {
            return _DBContext.Set<T>();
        }
        #endregion 
    }
}
