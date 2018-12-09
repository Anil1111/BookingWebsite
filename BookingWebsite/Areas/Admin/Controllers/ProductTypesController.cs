using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingWebsite.Data;
using BookingWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        
        /// <summary>
        /// application dbcontext object for crud operations, with .net core we using dependency injection
        /// </summary>
        private readonly ApplicationDbContext _db;


        
        /// <summary>
        /// constructor for dependency injection
        /// retrieve applicationdbcontext using dependency injection
        /// and populate this db inside readonly object 
        /// </summary>
        /// <param name="db"></param>
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            // using entity framework to access database and convert to list and pass it to the view
            return View(_db.ProductTypes.ToList());
        }


        // GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create action method

        [HttpPost]

        // build-in asp.net functionality
        // With each request of httpPost AntiForgeryToken is added and passes along with the request
        // Once it reaches the server it is checked if its valid (not be altered)
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            //  Modelstate.IsValid - validation check on the server side if model is valid.
            //   we can have meany attributes assigned to properties in out models
            // asp.net will check against those attributes 
            if (ModelState.IsValid)
            {
                // if it is valid we will retrieved information to the database
                _db.Add(productTypes);
                // once addad we save changes to database
                await _db.SaveChangesAsync();
                // return to index with product types ; using "nameof" helps prevent spelling errors
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }


        // GET Edit Action Method - retrieving Id which user wants to edit
        public async Task<IActionResult> Edit(int? id)
        {
            // if id is null return nor found
            if (id == null)
            {
                return NotFound();

            }
            // if Id is not null, retrieve product type from database
            var productType = await _db.ProductTypes.FindAsync(id);
            // check if producType is null, return not found
            if (productType == null)
            {
                return NotFound();

            }
            // if producType is not null return vieW passing productType to the edit view
            return View(productType);
        }

        //POST Edit action method

        [HttpPost]

        // build-in asp.net functionality
        // With each request of httpPost AntiForgeryToken is added and passes along with the request
        // Once it reaches the server it is checked if its valid (not be altered)
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypes productTypes)
        {
            

            // check if passed id is equal to producTypes id
            if (id != productTypes.Id)
            {
                return NotFound();
            }


            //  Modelstate.IsValid - validation check on the server side if model is valid.
            //   we can have meany attributes assigned to properties in out models
            // asp.net will check against those attributes 
            if (ModelState.IsValid)
            {
                // update
                _db.Update(productTypes);
                //save changes
                await _db.SaveChangesAsync();
                // return to index with product types ; using "nameof" helps prevent spelling errors
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }



        // GET Delete Action Method - retrieving Id which user wants to edit
        public async Task<IActionResult> Delete(int? id)
        {
            // if id is null return nor found
            if (id == null)
            {
                return NotFound();

            }
            // if Id is not null, retrieve product type from database
            var productType = await _db.ProductTypes.FindAsync(id);
            // check if producType is null, return not found
            if (productType == null)
            {
                return NotFound();

            }
            // if producType is not null return vieW passing productType to the delete view
            return View(productType);
        }

        //POST Delete action method

        [HttpPost, ActionName("Delete")]

        // build-in asp.net functionality
        // With each request of httpPost AntiForgeryToken is added and passes along with the request
        // Once it reaches the server it is checked if its valid (not be altered)
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) // all we need to delete is an Id
        {

            // getting id from database
            var productTypes = await _db.ProductTypes.FindAsync(id);
            //once we hav an id we just need to remove
            _db.ProductTypes.Remove(productTypes);
            //save changes
            await _db.SaveChangesAsync();
            // return to index with product types ; using "nameof" helps prevent spelling errors
            return RedirectToAction(nameof(Index));
        }

        // GET Details Action Method - retrieving Id which user wants to view
        public async Task<IActionResult> Details(int? id)
        {
            // if id is null return nor found
            if (id == null)
            {
                return NotFound();

            }
            // if Id is not null, retrieve product type from database
            var productType = await _db.ProductTypes.FindAsync(id);
            // check if productType is null, return not found
            if (productType == null)
            {
                return NotFound();

            }

            return View(productType);

        }


    }
        
}
