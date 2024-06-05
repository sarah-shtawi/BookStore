using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var categories = context.categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            if(!ModelState.IsValid) 
            {
                return View("Create", categoryVM); 
            }
            var Category = new Category
            {
               Name = categoryVM.Name
            };
            context.categories.Add(Category);//DB
            context.SaveChanges();
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult Edit(CategoryVM categoryvm)
        {
            var Category = context.categories.Find(categoryvm.Id);
            if(Category is null)
            {
                return NotFound();
            }
            Category.Name = categoryvm.Name;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
