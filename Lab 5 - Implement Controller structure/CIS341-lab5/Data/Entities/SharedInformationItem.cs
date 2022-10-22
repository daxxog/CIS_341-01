using System;
using System.Collections.Generic;

namespace CIS341_lab5.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'shared_information_item'.
    /// </summary>
    public partial class SharedInformationItem
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
        public long Id { get; set; }

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

    }
}
