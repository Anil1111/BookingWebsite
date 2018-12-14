using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWebsite.Models.ViewModel
{
    public class ProductsViewModel
    {
        public Products Products { get; set; }

        [Display(Name = "Property Type")]
        public IEnumerable<ProductTypes> ProductTypes { get; set; }

        [Display(Name = "Tag")]
        public IEnumerable<Tags> Tags { get; set; }



    }
}
