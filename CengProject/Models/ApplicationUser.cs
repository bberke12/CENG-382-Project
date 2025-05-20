using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CengProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; } = true;

    }
}
