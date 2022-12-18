using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CIS341_checkpoint3.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="CIS341_checkpoint3.Data.Entities.TaggedInformationItem" />.
    /// </summary>
    public static partial class TaggedInformationItemExtensions
    {
        #region Generated Extensions

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="informationItemId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<CIS341_checkpoint3.Data.Entities.TaggedInformationItem> ByInformationItemId(
            this IQueryable<CIS341_checkpoint3.Data.Entities.TaggedInformationItem> queryable, long informationItemId)
        {
            return queryable.Where(q => q.InformationItemId == informationItemId);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="tagName">The value to filter by.</param>
        /// <param name="informationItemId">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_checkpoint3.Data.Entities.TaggedInformationItem"/> or null if not found.</returns>
        public static CIS341_checkpoint3.Data.Entities.TaggedInformationItem GetByKey(
            this IQueryable<CIS341_checkpoint3.Data.Entities.TaggedInformationItem> queryable, string tagName,
            long informationItemId)
        {
            if (queryable is DbSet<CIS341_checkpoint3.Data.Entities.TaggedInformationItem> dbSet)
                return dbSet.Find(tagName, informationItemId);

            return queryable.FirstOrDefault(q => q.TagName == tagName
                                                 && q.InformationItemId == informationItemId);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="tagName">The value to filter by.</param>
        /// <param name="informationItemId">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_checkpoint3.Data.Entities.TaggedInformationItem"/> or null if not found.</returns>
        public static ValueTask<CIS341_checkpoint3.Data.Entities.TaggedInformationItem> GetByKeyAsync(
            this IQueryable<CIS341_checkpoint3.Data.Entities.TaggedInformationItem> queryable, string tagName,
            long informationItemId)
        {
            if (queryable is DbSet<CIS341_checkpoint3.Data.Entities.TaggedInformationItem> dbSet)
                return dbSet.FindAsync(tagName, informationItemId);

            var task = queryable.FirstOrDefaultAsync(q => q.TagName == tagName
                                                          && q.InformationItemId == informationItemId);
            return new ValueTask<CIS341_checkpoint3.Data.Entities.TaggedInformationItem>(task);
        }

        #endregion
    }
}