using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIS341_lab8.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'shared_information_item'.
    /// </summary>
    public partial class SharedInformationItem : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedInformationItem"/> class.
        /// </summary>
        public SharedInformationItem()
        {
            #region Generated Constructor

            InformationItemFavorites = new HashSet<Favorite>();
            InformationItemTaggedInformationItems = new HashSet<TaggedInformationItem>();

            #endregion
        }

        #region Generated Properties

        /// <summary>
        /// Gets or sets the property value representing column 'id'.
        /// </summary>
        /// <value>
        /// The property value representing column 'id'.
        /// </value>
        [Display(Name = "Shared Information Item ID")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'title'.
        /// </summary>
        /// <value>
        /// The property value representing column 'title'.
        /// </value>
        [Display(Name = "Shared Information Item Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'details'.
        /// </summary>
        /// <value>
        /// The property value representing column 'details'.
        /// </value>
        [Display(Name = "Details")]
        public string Details { get; set; }

        #endregion

        #region Generated Relationships

        /// <summary>
        /// Gets or sets the navigation collection for entity <see cref="Favorite" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="Favorite" />.
        /// </value>
        public virtual ICollection<Favorite> InformationItemFavorites { get; set; }

        /// <summary>
        /// Gets or sets the navigation collection for entity <see cref="TaggedInformationItem" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="TaggedInformationItem" />.
        /// </value>
        public virtual ICollection<TaggedInformationItem> InformationItemTaggedInformationItems { get; set; }

        #endregion

        // https://learn.microsoft.com/en-us/ef/ef6/saving/validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title.Length > 64)
            {
                yield return new ValidationResult(
                    "Title must be less than or equal to 64 characters in length",
                    new[] { nameof(Title) });
            }

            if (Details.Length > 4096)
            {
                yield return new ValidationResult(
                    "Description must be less than or equal to 4096 characters in length",
                    new[] { nameof(Details) });
            }
        }

        // merge using + operator (used in Update CRUD)
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading
        public static SharedInformationItem operator +(SharedInformationItem left, SharedInformationItem right)
        {
            left.Title = right.Title;
            left.Details = right.Details;
            left.InformationItemTaggedInformationItems = right.InformationItemTaggedInformationItems;

            return left;
        }
    }
}