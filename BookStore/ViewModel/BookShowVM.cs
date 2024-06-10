using BookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class BookShowVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
       
        public string? ImageUrl { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
    }
}
