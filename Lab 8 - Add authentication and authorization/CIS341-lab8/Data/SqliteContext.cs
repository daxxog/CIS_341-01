using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CIS341_lab8.Data
{
    /// <summary>
    /// A <see cref="DbContext" /> instance represents a session with the database and can be used to query and save instances of entities. 
    /// </summary>
    public partial class SqliteContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by this <see cref="DbContext" />.</param>
        public SqliteContext(DbContextOptions<SqliteContext> options)
            : base(options)
        {
            // https://stackoverflow.com/a/50042017
            Database.EnsureCreated();
        }

        // https://www.bricelam.net/2016/12/13/validation-in-efcore.html
        /*
        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                where e.State == EntityState.Added
                      || e.State == EntityState.Modified
                select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }

            return base.SaveChanges();
        }
        */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=sqlite.db");
        }

        #region Generated Properties

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.Favorite"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.Favorite"/>.
        /// </value>
        public virtual DbSet<CIS341_lab8.Data.Entities.Favorite> Favorites { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.SharedInformationItem"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.SharedInformationItem"/>.
        /// </value>
        public virtual DbSet<CIS341_lab8.Data.Entities.SharedInformationItem> SharedInformationItems { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.TaggedInformationItem"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.TaggedInformationItem"/>.
        /// </value>
        public virtual DbSet<CIS341_lab8.Data.Entities.TaggedInformationItem> TaggedInformationItems { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.UserInformationItem"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.UserInformationItem"/>.
        /// </value>
        public virtual DbSet<CIS341_lab8.Data.Entities.UserInformationItem> UserInformationItems { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.User"/>.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> that can be used to query and save instances of <see cref="CIS341_lab8.Data.Entities.User"/>.
        /// </value>
        public virtual DbSet<CIS341_lab8.Data.Entities.User> Users { get; set; }

        #endregion

        /// <summary>
        /// Configure the model that was discovered from the entity types exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on this context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration

            modelBuilder.ApplyConfiguration(new CIS341_lab8.Data.Mapping.FavoriteMap());
            modelBuilder.ApplyConfiguration(new CIS341_lab8.Data.Mapping.SharedInformationItemMap());
            modelBuilder.ApplyConfiguration(new CIS341_lab8.Data.Mapping.TaggedInformationItemMap());
            modelBuilder.ApplyConfiguration(new CIS341_lab8.Data.Mapping.UserInformationItemMap());
            modelBuilder.ApplyConfiguration(new CIS341_lab8.Data.Mapping.UserMap());

            #endregion
        }
    }
}