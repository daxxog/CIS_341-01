using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CIS341_lab5.Data.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="CIS341_lab5.Data.Entities.TaggedInformationItem" />
    /// </summary>
    public partial class TaggedInformationItemMap
        : IEntityTypeConfiguration<CIS341_lab5.Data.Entities.TaggedInformationItem>
    {
        /// <summary>
        /// Configures the entity of type <see cref="CIS341_lab5.Data.Entities.TaggedInformationItem" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CIS341_lab5.Data.Entities.TaggedInformationItem> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("tagged_information_item");

            // key
            builder.HasKey(t => new { t.TagName, t.InformationItemId });

            // properties
            builder.Property(t => t.TagName)
                .IsRequired()
                .HasColumnName("tag_name")
                .HasColumnType("VARCHAR");

            builder.Property(t => t.InformationItemId)
                .IsRequired()
                .HasColumnName("information_item_id")
                .HasColumnType("INTEGER");

            // relationships
            builder.HasOne(t => t.InformationItemSharedInformationItem)
                .WithMany(t => t.InformationItemTaggedInformationItems)
                .HasForeignKey(d => d.InformationItemId)
                .HasConstraintName("1");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="CIS341_lab5.Data.Entities.TaggedInformationItem" /></summary>
            public const string Schema = "";
            /// <summary>Table Name constant for entity <see cref="CIS341_lab5.Data.Entities.TaggedInformationItem" /></summary>
            public const string Name = "tagged_information_item";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="CIS341_lab5.Data.Entities.TaggedInformationItem.TagName" /></summary>
            public const string TagName = "tag_name";
            /// <summary>Column Name constant for property <see cref="CIS341_lab5.Data.Entities.TaggedInformationItem.InformationItemId" /></summary>
            public const string InformationItemId = "information_item_id";
        }
        #endregion
    }
}
