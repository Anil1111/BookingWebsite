using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWebsite.Models.ViewModel
{
    public class ProductsViewModel
    {
        public Products Products { get; set; }

        public IEnumerable<ProductTypes> ProductTypes { get; set; }
        public IEnumerable<Tags> Tags { get; set; }



    }
}
