using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SubsApi.Models
{
    public class AppUser : IdentityUser<int>
    {
        public DateOnly DateOfBirth { get; set; }
 //       public string Email { get; set; }   
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
  //      public Gender Gender { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; } 

    }
}
