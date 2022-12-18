using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CIS341_checkpoint3.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="CIS341_checkpoint3.Data.Entities.User" />.
    /// </summary>
    public static partial class UserExtensions
    {
        #region Generated Extensions

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_checkpoint3.Data.Entities.User"/> or null if not found.</returns>
        public static CIS341_checkpoint3.Data.Entities.User GetByKey(
            this IQueryable<CIS341_checkpoint3.Data.Entities.User> queryable,
            long id)
        {
            if (queryable is DbSet<CIS341_checkpoint3.Data.Entities.User> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_checkpoint3.Data.Entities.User"/> or null if not found.</returns>
        public static ValueTask<CIS341_checkpoint3.Data.Entities.User> GetByKeyAsync(
            this IQueryable<CIS341_checkpoint3.Data.Entities.User> queryable, long id)
        {
            if (queryable is DbSet<CIS341_checkpoint3.Data.Entities.User> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<CIS341_checkpoint3.Data.Entities.User>(task);
        }

        #endregion
    }
}