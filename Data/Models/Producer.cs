using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LocalMarket.Data.DataConstants.Producer;


namespace LocalMarket.Data.Models
{
    public class Producer
    {

        public Producer()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public string AboutMe { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        [Required]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public IEnumerable<Product> Products { get; init; }
    }
}
