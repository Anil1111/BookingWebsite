using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookingWebsite.Models
{
    public class Products
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }


        public bool Available { get; set; }

        public string Image { get; set; }


        // full;part;no
        public string FurnishDetail { get; set; }



        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }


        /// <summary>
        /// Linking ProductTypes to ProductTypeId as foreign key
        /// </summary>
        [ForeignKey("ProductTypeId")]
        public virtual ProductTypes ProductTypes { get; set; }




        [Display(Name = "Tag")]
        public int TagsId { get; set; }


        /// <summary>
        /// Linking Tags to TagsId as foreign key
        /// </summary>
        [ForeignKey("TagsId")]
        public virtual Tags Tags { get; set; }
    }
}
