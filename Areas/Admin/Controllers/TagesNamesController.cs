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
    public class TagesNamesController : Controller
    {
        private ApplicationDbContext _db;
        public TagesNamesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.tagesName.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagesName tages)
        {

            if (ModelState.IsValid)
            {
                _db.tagesName.Add(tages);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tages);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            TagesName t = _db.tagesName.Find(id);

            if (t == null)
                return NotFound();

            return View(t);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TagesName tages)
        {

            if (ModelState.IsValid)
            {
                _db.tagesName.Update(tages);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tages);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            TagesName t = _db.tagesName.Find(id);

            if (t == null)
                return NotFound();

            return View(t);

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var t = await _db.tagesName.FirstOrDefaultAsync(i => i.Id == id);

            if (t == null)
                return NotFound();

            return View(t);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            var t = await _db.tagesName.FindAsync(id);


            _db.tagesName.Remove(t);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");



        }


    }
}
