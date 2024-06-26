﻿using BookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class BookVM
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Title { get; set; }
        [Display(Name ="Author")]
        public int AuthorId { get; set; }
        public List <SelectListItem>? Authors { get; set; }

        public string Publisher { get; set; } = null!;
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Display(Name ="Your Image")]
        public IFormFile? ImageUrl { get; set; }

        public string Description { get; set; } = null!;

        public List<int> SelectedCategories { get; set; } = new List<int>();
        public List<SelectListItem>? Categories { get; set; }
    }
}
