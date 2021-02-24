using Expenses.Data;
using Expenses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this._db = db;
        }

        public IActionResult Index()
        {
           var expenses = _db.Expenses.ToList();
            return View(expenses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateExpense(Expense expense)
        {
            _db.Add(expense);
            _db.SaveChanges();
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var expense = _db.Expenses.Find(id);
            if (expense == null) return NotFound();
            return View(expense);
        }

        [HttpPost]
        public IActionResult Update(Expense expense)
        {
            if (expense == null) return NotFound();
            _db.Expenses.Update(expense);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var expense = _db.Expenses.Find(id);
            if (expense == null) return NotFound();
            return View(expense);
        }


        [HttpPost]
        public IActionResult DeleteExpense(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();
            var expense = _db.Expenses.Find(id);
            if (expense == null)
                return NotFound();
                _db.Expenses.Remove(expense);
                _db.SaveChanges();
     
            return Redirect("Index");
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
    }
}
