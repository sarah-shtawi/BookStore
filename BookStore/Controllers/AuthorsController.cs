using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext context;

        public AuthorsController  (ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var authors = context.Authors.ToList();

            var AuthoeVM = authors.Select(author => new AuthorVM
            {
                Id = author.Id,
                Name = author.Name,
                CreatedOn = author.CreatedOn,
                UpdatedOn = author.UpdatedOn,

            }).ToList();
            return View(AuthoeVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("AuthorForm");
        }

        [HttpPost]
        public IActionResult Create(AuthorFormVM authorvm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", authorvm);
            }
            var author = new Author
            {
                Id = authorvm.Id,
                Name = authorvm.Name,
                
            };
            context.Authors.Add(author);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Author = context.Authors.Find(id);
            if(Author is null)
            {
                return NotFound();
            }
            var AuthorVM = new AuthorFormVM
            {
                Id = Author.Id,
                Name = Author.Name,
            };
            return View("AuthorForm", AuthorVM);

        }

        [HttpPost]
        public IActionResult Edit(AuthorFormVM authorvm)
        {
            if (!ModelState.IsValid)
            {
                return View("AuthorForm", authorvm);
            }
            var Author = context.Authors.Find(authorvm.Id);
            if( Author is null)
            {
                return NotFound();
            }
            Author.Name = authorvm.Name;
            Author.UpdatedOn = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Details(int id)
        {
            var Author = context.Authors.Find(id);
            if(Author is null)
            {
                return NotFound();
            }
            var AuthorVM = new AuthorVM
            {
                Id =Author.Id,
                Name = Author.Name,
                CreatedOn = Author.CreatedOn,
                UpdatedOn = Author.UpdatedOn,
            };
            return View(AuthorVM);

        }
        public IActionResult Delete(int id)
        {
            var Author = context.Authors.Find(id);
            if(Author is null)
            {
                return NotFound();
            }
            context.Authors.Remove(Author);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
