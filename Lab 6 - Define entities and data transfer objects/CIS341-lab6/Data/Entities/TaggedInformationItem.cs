using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIS341_lab6.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'tagged_information_item'.
    /// </summary>
    public partial class TaggedInformationItem
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
    }
}