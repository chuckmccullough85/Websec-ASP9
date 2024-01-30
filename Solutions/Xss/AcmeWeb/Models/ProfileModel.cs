using AcmeLib;
using System.ComponentModel.DataAnnotations;

namespace AcmeWeb.Models
{
    public class ProfileModel
    {
        public ProfileModel()
        { }

        public ProfileModel(User user)
        {
            FirstName = user.Firstname;
            LastName = user.Lastname;
            Email = user.Email;
            Phone = user.Phone;
        }

        [Required]
        [RegularExpression(@"[\w\s'-]{3,15}")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"[\w\s'-]{3,15}")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
    }
}
