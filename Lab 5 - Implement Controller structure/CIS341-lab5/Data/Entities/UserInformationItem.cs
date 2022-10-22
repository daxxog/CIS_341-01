using System;
using System.Collections.Generic;

namespace CIS341_lab5.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'user_information_item'.
    /// </summary>
    public partial class UserInformationItem
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
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'user_id'.
        /// </summary>
        /// <value>
        /// The property value representing column 'user_id'.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'title'.
        /// </summary>
        /// <value>
        /// The property value representing column 'title'.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'details'.
        /// </summary>
        /// <value>
        /// The property value representing column 'details'.
        /// </value>
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
        public virtual User User { get; set; }

        #endregion

    }
}
