using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.SessionState;

namespace WebApplication1.Controllers
{
     [Area("Customers")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.products.Include(c=>c.prodType).Include(c=>c.Tages).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public IActionResult Details(int ? id)
        {

            if (id == null)
                return NotFound();

            var prod = _db.products.Include(c => c.prodType).Where(k => k.Id == id).FirstOrDefault();
            if (prod == null)
                return NotFound();
            return View(prod);
        }

        [HttpPost]
        public IActionResult Details(int id)
        {
            List<Products> products = new List<Products>();

            if (id == null)
            { return NotFound(); }

            var prod = _db.products.Include(c => c.prodType).Where(k => k.Id == id).FirstOrDefault();

            if (prod == null)
            { return NotFound(); }

            products = HttpContext.Session.Get<List<Products>>("products");

            if (products == null)
            { products = new List<Products>(); }

            products.Add(prod);
            HttpContext.Session.Set("products",products);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Remove(int ?id)
        {

         List<Products> prod=HttpContext.Session.Get<List<Products>>("products");
            if(prod!=null)

            {
                var p = prod.FirstOrDefault(k => k.Id == id);
                prod.Remove(p);
                HttpContext.Session.Set("products", prod);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {

          List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
                return NotFound();
            return View(products);
        }

    }
}
