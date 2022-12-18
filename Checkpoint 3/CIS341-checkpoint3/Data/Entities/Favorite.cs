using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIS341_checkpoint3.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'favorites'.
    /// </summary>
    public partial class Favorite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Favorite"/> class.
        /// </summary>
        public Favorite()
        {
            #region Generated Constructor

            #endregion
        }

        #region Generated Properties

        /// <summary>
        /// Gets or sets the property value representing column 'user_id'.
        /// </summary>
        /// <value>
        /// The property value representing column 'user_id'.
        /// </value>
        [Display(Name = "User ID")]
        public long? UserId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'information_item_id'.
        /// </summary>
        /// <value>
        /// The property value representing column 'information_item_id'.
        /// </value>
        [Display(Name = "Information Item ID")]
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
        [Display(Name = "Shared Information Item")]
        public virtual SharedInformationItem? InformationItemSharedInformationItem { get; set; }

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
    }
}