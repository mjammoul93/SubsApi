using System.ComponentModel.DataAnnotations;

namespace SubsApi.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }

        public string Email { get; set; }  
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } 
        public string[] Roles { get; set; }
    }
}
