using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIS341_checkpoint2.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'user_information_item'.
    /// </summary>
    public partial class UserInformationItem : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInformationItem"/> class.
        /// </summary>
        public UserInformationItem()
        {
            #region Generated Constructor

            #endregion
        }

        #region Generated Properties

        /// <summary>
        /// Gets or sets the property value representing column 'id'.
        /// </summary>
        /// <value>
        /// The property value representing column 'id'.
        /// </value>
        [Display(Name = "Information Item ID")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'user_id'.
        /// </summary>
        /// <value>
        /// The property value representing column 'user_id'.
        /// </value>
        [Display(Name = "User ID")]
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'title'.
        /// </summary>
        /// <value>
        /// The property value representing column 'title'.
        /// </value>
        [Display(Name = "Title")]
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
        /// Gets or sets the navigation property for entity <see cref="User" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="User" />.
        /// </value>
        /// <seealso cref="UserId" />
        [Display(Name = "User")]
        public virtual User? User { get; set; }

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
    }
}