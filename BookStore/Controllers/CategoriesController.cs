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
            var CategoriesVM = categories.Select(category => new CategoryFormVM
            {
                Id = category.Id,
                Name = category.Name,
              
            }).ToList();
            return View(CategoriesVM);
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
            try
            {
                context.categories.Add(Category);//DB
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Category name is already exits");
                return View(categoryVM);

            }
            
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = context.categories.Find(Id);
            if (category is null)
            {
                return NotFound();
            }
            var categoryVM = new CategoryFormVM
            { 
                Id =Id,
                Name = category.Name };
            return View("Create", categoryVM);
        }
        [HttpPost]
        public IActionResult Edit(CategoryFormVM categoryvm)
        {
            if(!ModelState.IsValid) 
            {
                return View("Create", categoryvm);
            }
            var Category = context.categories.Find(categoryvm.Id);
            if(Category is null)
            {
                return NotFound();
            }
            Category.Name = categoryvm.Name;
            Category.UpdatedOn = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Details(int Id) 
        {
            var category = context.categories.Find(Id);
            if (category is null)
            {
                return NotFound();
            }
            var categoryVM = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                CreatedOn = category.CreatedOn,
                UpdatedOn = category.UpdatedOn
            };
            return View(categoryVM);
        }
        public IActionResult Delete(int id)
        {
            var category = context.categories.Find(id);
            if(category is null) 
            {
                return NotFound();
            }
            context.categories.Remove(category);
            context.SaveChanges();
            return Ok();
        }
        public IActionResult CheakName(CategoryVM categoryvm)
        {
            var isExsits = context.categories.Any(category => categoryvm.Name == category.Name); 
            return Json(!isExsits);
        }
    }
}
