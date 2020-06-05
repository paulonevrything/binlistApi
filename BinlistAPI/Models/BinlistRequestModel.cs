using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistAPI.Models
{
    public class BinlistRequestModel
    {
        [Required]
        [StringLength(8, ErrorMessage = "Card IIN must be 6 or 8 digits long", MinimumLength = 6)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid IIN number supplied, IIN number must be a numbers only with no special characters")]
        public string cardIin { get; set; }
    }
}
