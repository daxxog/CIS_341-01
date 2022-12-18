using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CIS341_checkpoint3.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="CIS341_checkpoint3.Data.Entities.UserInformationItem" />.
    /// </summary>
    public static partial class UserInformationItemExtensions
    {
        #region Generated Extensions

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_checkpoint3.Data.Entities.UserInformationItem"/> or null if not found.</returns>
        public static CIS341_checkpoint3.Data.Entities.UserInformationItem GetByKey(
            this IQueryable<CIS341_checkpoint3.Data.Entities.UserInformationItem> queryable, long id)
        {
            if (queryable is DbSet<CIS341_checkpoint3.Data.Entities.UserInformationItem> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_checkpoint3.Data.Entities.UserInformationItem"/> or null if not found.</returns>
        public static ValueTask<CIS341_checkpoint3.Data.Entities.UserInformationItem> GetByKeyAsync(
            this IQueryable<CIS341_checkpoint3.Data.Entities.UserInformationItem> queryable, long id)
        {
            if (queryable is DbSet<CIS341_checkpoint3.Data.Entities.UserInformationItem> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<CIS341_checkpoint3.Data.Entities.UserInformationItem>(task);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<CIS341_checkpoint3.Data.Entities.UserInformationItem> ByUserId(
            this IQueryable<CIS341_checkpoint3.Data.Entities.UserInformationItem> queryable, long userId)
        {
            return queryable.Where(q => q.UserId == userId);
        }

        #endregion
    }
}