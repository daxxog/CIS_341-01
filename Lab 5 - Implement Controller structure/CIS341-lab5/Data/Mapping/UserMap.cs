using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CIS341_lab5.Data.Mapping
{
    /// <summary>
    /// Allows configuration for an entity type <see cref="CIS341_lab5.Data.Entities.User" />
    /// </summary>
    public partial class UserMap
        : IEntityTypeConfiguration<CIS341_lab5.Data.Entities.User>
    {
        /// <summary>
        /// Configures the entity of type <see cref="CIS341_lab5.Data.Entities.User" />
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(
            Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CIS341_lab5.Data.Entities.User> builder)
        {
            #region Generated Configure

            // table
            builder.ToTable("user");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("INTEGER");

            builder.Property(t => t.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("VARCHAR");

            builder.Property(t => t.PasswordHash)
                .IsRequired()
                .HasColumnName("password_hash")
                .HasColumnType("VARCHAR");

            builder.Property(t => t.ContentManager)
                .IsRequired()
                .HasColumnName("content_manager")
                .HasColumnType("BOOLEAN");

            // relationships

            #endregion
        }

        #region Generated Constants

        public struct Table
        {
            /// <summary>Table Schema name constant for entity <see cref="CIS341_lab5.Data.Entities.User" /></summary>
            public const string Schema = "";

            /// <summary>Table Name constant for entity <see cref="CIS341_lab5.Data.Entities.User" /></summary>
            public const string Name = "user";
        }

        public struct Columns
        {
            /// <summary>Column Name constant for property <see cref="CIS341_lab5.Data.Entities.User.Id" /></summary>
            public const string Id = "id";

            /// <summary>Column Name constant for property <see cref="CIS341_lab5.Data.Entities.User.Email" /></summary>
            public const string Email = "email";

            /// <summary>Column Name constant for property <see cref="CIS341_lab5.Data.Entities.User.PasswordHash" /></summary>
            public const string PasswordHash = "password_hash";

            /// <summary>Column Name constant for property <see cref="CIS341_lab5.Data.Entities.User.ContentManager" /></summary>
            public const string ContentManager = "content_manager";
        }

        #endregion
    }
}