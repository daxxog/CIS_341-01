using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CIS341_checkpoint3.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the property value representing column 'user_id'.
    /// </summary>
    /// <value>
    /// The property value representing column 'user_id'.
    /// </value>
    public long UserId { get; set; }
}