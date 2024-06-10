using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnviroment;

        public BooksController (ApplicationDbContext context,IWebHostEnvironment webHostEnviroment)
        {
            this.context = context;
            this.webHostEnviroment = webHostEnviroment;
        }
        public IActionResult Index()
        {
            var Books = context.Books.Include(book=> book.Author).
                        Include(book => book.Categories). //list
                        ThenInclude(book => book.Category) //obj
                       .ToList();

            var BooksVM = Books.Select(Book => new BookShowVM
            {
                Id = Book.Id,
                Title = Book.Title,
                Author = Book.Author.Name,
                ImageUrl = Book.ImageUrl,
                Categories = Book.Categories.Select( Book => Book.Category.Name).ToList(),
            }).ToList();
            return View(BooksVM);
        }

      
        [HttpGet]
        public IActionResult Create()
        {
            var authors = context.Authors.OrderBy(author => author.Name).ToList();
            var authorsList = new List<SelectListItem>();

            var Categories = context.categories.OrderBy(Category => Category.Name).ToList();
            var CategoriesList = new List<SelectListItem>(); 

            foreach (var author in authors)
            {
                authorsList.Add(new SelectListItem
                {
                    Value = author.Id.ToString(),
                    Text = author.Name
                });
            }
            foreach(var category in Categories)
            {
                CategoriesList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
            var ViewModel = new BookVM
            {
                Authors = authorsList,
                Categories = CategoriesList
            };
            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Create (BookVM bookvm)
        {
            if (!ModelState.IsValid)
            {
                return View(bookvm);
            }
            var ImageName = "";
            if (bookvm.ImageUrl != null)
            {
                ImageName = Path.GetFileName(bookvm.ImageUrl.FileName);
                var ImagePath = Path.Combine($"{webHostEnviroment.WebRootPath}/img/Books", ImageName);
                var stream = System.IO.File.Create(ImagePath);
                bookvm.ImageUrl.CopyTo(stream);
            }
            var Book = new Book
            {
                Title = bookvm.Title,
                AuthorId = bookvm.AuthorId,
                Publisher = bookvm.Publisher,
                PublishDate = bookvm.PublishDate,
                Description = bookvm.Description,
                ImageUrl = ImageName,
                Categories = bookvm.SelectedCategories.Select(id => new BookCategory
                {
                    CategoryId = id,
                }).ToList(),
            };
            context.Books.Add(Book);
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            var Book = context.Books.Find(id);
            if (Book is null)
            {
                return NotFound();
            }
            var path = Path.Combine(webHostEnviroment.WebRootPath, "img/books", Book.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            context.Books.Remove(Book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
