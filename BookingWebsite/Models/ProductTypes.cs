using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWebsite.Models
{
    public class ProductTypes
    {
        // Id - primary key
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
