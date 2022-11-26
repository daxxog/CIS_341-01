using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CIS341_lab9.Data.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="CIS341_lab9.Data.Entities.UserInformationItem" />
    /// </summary>
    public partial class UserInformationItemMap
        : IEntityTypeConfiguration<CIS341_lab9.Data.Entities.UserInformationItem>
    {
        /// <summary>
        /// Configures the entity of type <see cref="CIS341_lab9.Data.Entities.UserInformationItem" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(
            Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<
                CIS341_lab9.Data.Entities.UserInformationItem> builder)
        {
            #region Generated Configure

            // table
            builder.ToTable("user_information_item");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("INTEGER");

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("user_id")
                .HasColumnType("INTEGER");

            builder.Property(t => t.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasColumnType("VARCHAR");

            builder.Property(t => t.Details)
                .IsRequired()
                .HasColumnName("details")
                .HasColumnType("VARCHAR");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.UserInformationItems)
                .HasForeignKey(d => d.UserId);

            #endregion
        }

        #region Generated Constants

        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="CIS341_lab9.Data.Entities.UserInformationItem" /></summary>
            public const string Schema = "";

            /// <summary>Table Name constant for entity <see cref="CIS341_lab9.Data.Entities.UserInformationItem" /></summary>
            public const string Name = "user_information_item";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="CIS341_lab9.Data.Entities.UserInformationItem.Id" /></summary>
            public const string Id = "id";

            /// <summary>Column Name constant for property <see cref="CIS341_lab9.Data.Entities.UserInformationItem.UserId" /></summary>
            public const string UserId = "user_id";

            /// <summary>Column Name constant for property <see cref="CIS341_lab9.Data.Entities.UserInformationItem.Title" /></summary>
            public const string Title = "title";

            /// <summary>Column Name constant for property <see cref="CIS341_lab9.Data.Entities.UserInformationItem.Details" /></summary>
            public const string Details = "details";
        }

        #endregion
    }
}