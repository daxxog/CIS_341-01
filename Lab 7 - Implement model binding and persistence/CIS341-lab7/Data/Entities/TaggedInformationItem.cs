using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CIS341_lab7.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'tagged_information_item'.
    /// </summary>
    public partial class TaggedInformationItem : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaggedInformationItem"/> class.
        /// </summary>
        public TaggedInformationItem()
        {
            #region Generated Constructor

            #endregion
        }

        #region Generated Properties

        /// <summary>
        /// Gets or sets the property value representing column 'tag_name'.
        /// </summary>
        /// <value>
        /// The property value representing column 'tag_name'.
        /// </value>
        [Display(Name = "Tag Name")]
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'information_item_id'.
        /// </summary>
        /// <value>
        /// The property value representing column 'information_item_id'.
        /// </value>
        [Display(Name = "Shared Information Item ID")]
        public long InformationItemId { get; set; }

        #endregion

        #region Generated Relationships

        /// <summary>
        /// Gets or sets the navigation property for entity <see cref="SharedInformationItem" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="SharedInformationItem" />.
        /// </value>
        /// <seealso cref="InformationItemId" />
        public virtual SharedInformationItem InformationItemSharedInformationItem { get; set; }

        #endregion

        // https://learn.microsoft.com/en-us/ef/ef6/saving/validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string cleanTagName = Regex.Replace(TagName, @"[^\w\s]", string.Empty);
            if (!TagName.Equals(cleanTagName))
            {
                yield return new ValidationResult(
                    "TagName must only contain alphanumeric characters and spaces",
                    new[] { nameof(TagName) });
            }
        }
    }
}