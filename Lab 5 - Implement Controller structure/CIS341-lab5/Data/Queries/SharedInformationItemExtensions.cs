using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CIS341_lab5.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="CIS341_lab5.Data.Entities.SharedInformationItem" />.
    /// </summary>
    public static partial class SharedInformationItemExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_lab5.Data.Entities.SharedInformationItem"/> or null if not found.</returns>
        public static CIS341_lab5.Data.Entities.SharedInformationItem GetByKey(this IQueryable<CIS341_lab5.Data.Entities.SharedInformationItem> queryable, long id)
        {
            if (queryable is DbSet<CIS341_lab5.Data.Entities.SharedInformationItem> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:CIS341_lab5.Data.Entities.SharedInformationItem"/> or null if not found.</returns>
        public static ValueTask<CIS341_lab5.Data.Entities.SharedInformationItem> GetByKeyAsync(this IQueryable<CIS341_lab5.Data.Entities.SharedInformationItem> queryable, long id)
        {
            if (queryable is DbSet<CIS341_lab5.Data.Entities.SharedInformationItem> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<CIS341_lab5.Data.Entities.SharedInformationItem>(task);
        }

        #endregion

    }
}
