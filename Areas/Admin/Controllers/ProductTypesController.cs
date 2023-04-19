using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductTypesController : Controller
    {
       private ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.productsTypes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType productT)
        {

            if (ModelState.IsValid)
            {
               _db.productsTypes.Add(productT);
            await    _db.SaveChangesAsync();

                TempData["Save"] = "ProductType is saved succesfully";
                return RedirectToAction("Index");
            }

            return View(productT);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

          ProductType p= _db.productsTypes.Find(id);

            if (p == null)
                return NotFound();
            
            return View(p);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductType productT)
        {

            if (ModelState.IsValid)
            {
                _db.productsTypes.Update(productT);
              await   _db.SaveChangesAsync();
                TempData["edit"] = "item is edit successfully";
                return RedirectToAction("Index");
            }

            return View(productT);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            ProductType p = _db.productsTypes.Find(id);

            if (p == null)
                return NotFound();

            return View(p);

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var p = await _db.productsTypes.FirstOrDefaultAsync(i=>i.Id==id);

            if (p == null)
                return NotFound();

            TempData["Del"] = "Item is Deleted";
            return View(p);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            ProductType p =await _db.productsTypes.FindAsync(id);
          

            _db.productsTypes.Remove(p);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
             

            
        }


    }
}
