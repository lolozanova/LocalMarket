using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LocalMarket.Data.DataConstants.Producer;

namespace LocalMarket.Models.Producer
{
    public class CreateProducerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength= NameMinLength)]
        public string LastName { get; set; }

        [Required]
         [RegularExpression(PhoneNumberRegex, ErrorMessage = "Invalid phone number. It starts with +359 and is followed by 9 digits")]
         [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public int TownId { get; set; }

        public IEnumerable<TownViewModel> Towns { get; set; }

    }
}

