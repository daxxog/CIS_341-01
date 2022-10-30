using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIS341_lab6.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'user'.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            #region Generated Constructor

            Favorites = new HashSet<Favorite>();
            UserInformationItems = new HashSet<UserInformationItem>();

            #endregion
        }

        #region Generated Properties

        /// <summary>
        /// Gets or sets the property value representing column 'id'.
        /// </summary>
        /// <value>
        /// The property value representing column 'id'.
        /// </value>
        [Display(Name = "User ID")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'email'.
        /// </summary>
        /// <value>
        /// The property value representing column 'email'.
        /// </value>
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'password_hash'.
        /// </summary>
        /// <value>
        /// The property value representing column 'password_hash'.
        /// </value>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'content_manager'.
        /// </summary>
        /// <value>
        /// The property value representing column 'content_manager'.
        /// </value>
        [Display(Name = "Is Content Manager")]
        public bool ContentManager { get; set; }

        #endregion

        #region Generated Relationships

        /// <summary>
        /// Gets or sets the navigation collection for entity <see cref="Favorite" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="Favorite" />.
        /// </value>
        [Display(Name = "Favorites")]
        public virtual ICollection<Favorite> Favorites { get; set; }

        /// <summary>
        /// Gets or sets the navigation collection for entity <see cref="UserInformationItem" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="UserInformationItem" />.
        /// </value>
        [Display(Name = "My Information Items")]
        public virtual ICollection<UserInformationItem> UserInformationItems { get; set; }

        #endregion
    }
}