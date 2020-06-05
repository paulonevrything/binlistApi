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
        [StringLength(16, ErrorMessage = "Card IIN must be 6 or 8 digits long", MinimumLength = 8)]
        public string cardIin { get; set; }
    }
}
