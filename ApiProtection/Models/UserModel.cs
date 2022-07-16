using System.ComponentModel.DataAnnotations;

namespace ApiProtection.Models
{
    public class UserModel
    {
        [Range(1,int.MaxValue)]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
        
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }
        
        [Url]
        public string Website { get; set; }
        
        [Range(0,40)]
        public int NumberOfVehicles { get; set; }
    }
}
