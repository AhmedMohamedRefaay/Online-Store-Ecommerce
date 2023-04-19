using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _he;
        public ProductController(ApplicationDbContext db,IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.products.Include(c=>c.prodType).Include(t=>t.Tages).ToList());
        }

        [HttpPost]
        public IActionResult Index(decimal? LowAmount,decimal ?LargAmount)
        {
            var pr = _db.products.Include(c => c.prodType).Include(t => t.Tages).Where(a => a.Price >= LowAmount
              && a.Price <= LargAmount).ToList();

            if(LargAmount==null || LargAmount==null)
                pr= _db.products.Include(c => c.prodType).Include(t => t.Tages).ToList();
            return View(pr);
        }

        public  IActionResult Create()
        {
            ViewBag.ProductTypeId = new SelectList(_db.productsTypes.ToList(), "Id", "productType");

            ViewBag.TageNameId= new SelectList(_db.tagesName.ToList(), "Id", "Name");

            return View();
        } 
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult >Create(Products product,IFormFile img)
        {
            ViewBag.ProductTypeId = new SelectList(_db.productsTypes.ToList(), "Id", "productType");

            ViewBag.TageNameId = new SelectList(_db.tagesName.ToList(), "Id", "Name");

            var search = _db.products.Where(c => c.Name == product.Name).FirstOrDefault();
            
            if (ModelState.IsValid)
            {
                if(search!=null)
                {
                    ViewBag.Found = "This Product is already found";
                    return View(product);
                }

                if (img != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(img.FileName));
                    await img.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Img ="Images/"+ img.FileName;
                }
               if(img==null)
                {
                    product.Img = "Images/A.jpg";
                }
                    _db.products.Add(product);
                    await _db.SaveChangesAsync();         
                    return RedirectToAction("Index");
                }
             
            return View(product);
        }
        
        public IActionResult Edit(int?id)
        {
            ViewBag.ProductTypeId = new SelectList(_db.productsTypes.ToList(), "Id", "productType");

            ViewBag.TageNameId = new SelectList(_db.tagesName.ToList(), "Id", "Name");

            var product = _db.products.Include(c => c.prodType).Include(t => t.Tages).FirstOrDefault(c => c.Id == id);

            if (product == null)
                return NotFound();

            return View(product);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products product, IFormFile img)
        {
            ViewBag.ProductTypeId = new SelectList(_db.productsTypes.ToList(), "Id", "productType");

            ViewBag.TageNameId = new SelectList(_db.tagesName.ToList(), "Id", "Name");

            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(img.FileName));
                    await img.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Img = "Images/" + img.FileName;
                }
                if (img == null)
                {
                    product.Img = "Images/A.jpg";
                }
                _db.products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            Products p = _db.products.Include(c => c.prodType).Include(t => t.Tages).Where(t=>t.Id==id).FirstOrDefault();

            if (p == null)
                return NotFound();

            return View(p);

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var p = await _db.products.FirstOrDefaultAsync(i => i.Id == id);

            if (p == null)
                return NotFound();

            TempData["Del"] = "Item is Deleted";
            return View(p);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int ?id)
        {
            if (id == null)
                return NotFound();
            Products p = await _db.products.FindAsync(id);
            if (p == null)
                return NotFound();


            _db.products.Remove(p);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");



        }



    }
}
