using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CIS341_checkpoint2.Data.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="CIS341_checkpoint2.Data.Entities.SharedInformationItem" />
    /// </summary>
    public partial class SharedInformationItemMap
        : IEntityTypeConfiguration<CIS341_checkpoint2.Data.Entities.SharedInformationItem>
    {
        /// <summary>
        /// Configures the entity of type <see cref="CIS341_checkpoint2.Data.Entities.SharedInformationItem" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(
            Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<
                CIS341_checkpoint2.Data.Entities.SharedInformationItem> builder)
        {
            #region Generated Configure

            // table
            builder.ToTable("shared_information_item");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
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

            #endregion
        }

        #region Generated Constants

        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="CIS341_checkpoint2.Data.Entities.SharedInformationItem" /></summary>
            public const string Schema = "";

            /// <summary>Table Name constant for entity <see cref="CIS341_checkpoint2.Data.Entities.SharedInformationItem" /></summary>
            public const string Name = "shared_information_item";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="CIS341_checkpoint2.Data.Entities.SharedInformationItem.Id" /></summary>
            public const string Id = "id";

            /// <summary>Column Name constant for property <see cref="CIS341_checkpoint2.Data.Entities.SharedInformationItem.Title" /></summary>
            public const string Title = "title";

            /// <summary>Column Name constant for property <see cref="CIS341_checkpoint2.Data.Entities.SharedInformationItem.Details" /></summary>
            public const string Details = "details";
        }

        #endregion
    }
}