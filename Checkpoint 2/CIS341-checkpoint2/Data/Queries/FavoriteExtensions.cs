using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CIS341_checkpoint2.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="CIS341_checkpoint2.Data.Entities.Favorite" />.
    /// </summary>
    public static partial class FavoriteExtensions
    {
        #region Generated Extensions

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="informationItemId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<CIS341_checkpoint2.Data.Entities.Favorite> ByInformationItemId(
            this IQueryable<CIS341_checkpoint2.Data.Entities.Favorite> queryable, long informationItemId)
        {
            return queryable.Where(q => q.InformationItemId == informationItemId);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<CIS341_checkpoint2.Data.Entities.Favorite> ByUserId(
            this IQueryable<CIS341_checkpoint2.Data.Entities.Favorite> queryable, long userId)
        {
            return queryable.Where(q => q.UserId == userId);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userId">The value to filter by.</param>
        /// <param name="informationItemId">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_checkpoint2.Data.Entities.Favorite"/> or null if not found.</returns>
        public static CIS341_checkpoint2.Data.Entities.Favorite GetByKey(
            this IQueryable<CIS341_checkpoint2.Data.Entities.Favorite> queryable, long userId, long informationItemId)
        {
            if (queryable is DbSet<CIS341_checkpoint2.Data.Entities.Favorite> dbSet)
                return dbSet.Find(userId, informationItemId);

            return queryable.FirstOrDefault(q => q.UserId == userId
                                                 && q.InformationItemId == informationItemId);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userId">The value to filter by.</param>
        /// <param name="informationItemId">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_checkpoint2.Data.Entities.Favorite"/> or null if not found.</returns>
        public static ValueTask<CIS341_checkpoint2.Data.Entities.Favorite> GetByKeyAsync(
            this IQueryable<CIS341_checkpoint2.Data.Entities.Favorite> queryable, long userId, long informationItemId)
        {
            if (queryable is DbSet<CIS341_checkpoint2.Data.Entities.Favorite> dbSet)
                return dbSet.FindAsync(userId, informationItemId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserId == userId
                                                          && q.InformationItemId == informationItemId);
            return new ValueTask<CIS341_checkpoint2.Data.Entities.Favorite>(task);
        }

        #endregion
    }
}