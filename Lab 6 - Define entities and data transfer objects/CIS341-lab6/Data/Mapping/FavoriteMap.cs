using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CIS341_lab6.Data.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="CIS341_lab6.Data.Entities.Favorite" />
    /// </summary>
    public partial class FavoriteMap
        : IEntityTypeConfiguration<CIS341_lab6.Data.Entities.Favorite>
    {
        /// <summary>
        /// Configures the entity of type <see cref="CIS341_lab6.Data.Entities.Favorite" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(
            Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CIS341_lab6.Data.Entities.Favorite>
                builder)
        {
            #region Generated Configure

            // table
            builder.ToTable("favorites");

            // key
            builder.HasKey(t => new { t.UserId, t.InformationItemId });

            // properties
            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("user_id")
                .HasColumnType("INTEGER");

            builder.Property(t => t.InformationItemId)
                .IsRequired()
                .HasColumnName("information_item_id")
                .HasColumnType("INTEGER");

            // relationships
            builder.HasOne(t => t.InformationItemSharedInformationItem)
                .WithMany(t => t.InformationItemFavorites)
                .HasForeignKey(d => d.InformationItemId)
                .HasConstraintName("2");

            builder.HasOne(t => t.User)
                .WithMany(t => t.Favorites)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("3");

            #endregion
        }

        #region Generated Constants

        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="CIS341_lab6.Data.Entities.Favorite" /></summary>
            public const string Schema = "";

            /// <summary>Table Name constant for entity <see cref="CIS341_lab6.Data.Entities.Favorite" /></summary>
            public const string Name = "favorites";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="CIS341_lab6.Data.Entities.Favorite.UserId" /></summary>
            public const string UserId = "user_id";

            /// <summary>Column Name constant for property <see cref="CIS341_lab6.Data.Entities.Favorite.InformationItemId" /></summary>
            public const string InformationItemId = "information_item_id";
        }

        #endregion
    }
}