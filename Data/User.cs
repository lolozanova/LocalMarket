using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static LocalMarket.Data.DataConstants.User;


namespace LocalMarket.Data.Models
{
    public class User: IdentityUser
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string  FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; }

    }
}
