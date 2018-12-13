using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingWebsite.Data;
using BookingWebsite.Models;
using BookingWebsite.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingWebsite.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        // we need to access database
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ProductsViewModel ProductsVM { get; set; }


        // constructor
        public ProductsController(ApplicationDbContext db)
        {
            _db = db;

            // initialise ProductsViewModel
            ProductsVM = new ProductsViewModel()
            {
                ProductTypes = _db.ProductTypes.ToList(),
                Tags = _db.Tags.ToList(),
                Products = new Products()
            };

        }

        
        public async Task<IActionResult> Index()
        {


            var products = _db.Products.Include(m => m.ProductTypes).Include(m => m.Tags);
            return View(await products.ToListAsync());
        }
    }
}