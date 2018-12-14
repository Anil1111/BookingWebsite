using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookingWebsite.Data;
using BookingWebsite.Models;
using BookingWebsite.Models.ViewModel;
using BookingWebsite.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingWebsite.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        // we need to access database
        private readonly ApplicationDbContext _db;

        // 
        private readonly IHostingEnvironment _hostingEnvironment;



        // whenever we are post-ing or retrieving this will automatically bind this ProductsViewModel
        [BindProperty]
        public ProductsViewModel ProductsVM { get; set; }


        // constructor - retrieving db using dependency injection 
        public ProductsController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            // initialise ProductsViewModel
            ProductsVM = new ProductsViewModel()
            {
                // assign product types from db
                ProductTypes = _db.ProductTypes.ToList(),
                // tags
                Tags = _db.Tags.ToList(),
                Products = new Products()
            };

        }

        
        public async Task<IActionResult> Index()
        {

            // return list of products
            var products = _db.Products.Include(m => m.ProductTypes).Include(m => m.Tags);
            return View(await products.ToListAsync());
        }



        // Get : Product Create

        public IActionResult Create()
        {

            // passing ProductsVM (viewModel)
            return View(ProductsVM);
        }


        // POST : Product Create

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()  // note that because we have bind-ed ProductsViewModel we do not have to pass it here
        {
            if (!ModelState.IsValid)
            {
                return View(ProductsVM);

            }


            _db.Products.Add(ProductsVM.Products);
            await _db.SaveChangesAsync();

            // Image being saved
            string webRootPath = _hostingEnvironment.WebRootPath;

            // files will have files uploaded from the View
            var files = HttpContext.Request.Form.Files;




            // retrieve from database 
            var productsFromDb = _db.Products.Find(ProductsVM.Products.Id);


            // check for uploaded files
            if (files.Count != 0) 
            {
                // Image has been uploaded
                // find upload path
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                // find file extenstion
                var extenstion = Path.GetExtension(files[0].FileName);

                // using filesteram we will copy uploaded file to the server and rename it to product Id
                using (var filestream = new FileStream(Path.Combine(uploads, ProductsVM.Products.Id + extenstion), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                    
                }

                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.Id + extenstion;


            }
            else
            {
                // when user does not upload an image use default image
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath+ @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.Id + ".png");
                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.Id + ".png";
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        



    }
}