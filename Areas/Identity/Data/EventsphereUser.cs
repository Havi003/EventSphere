using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Eventsphere.Areas.Identity.Data;

// Add profile data for application users by adding properties to the EventsphereUser class
public class EventsphereUser : IdentityUser
{


    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string? FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string? LastName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string? DisplayName { get; set; }
}

