using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class CategoryVM
    {

        public int Id { get; set; }
        [MaxLength(30,ErrorMessage="Max Langth 30 characters")]
        [Required(ErrorMessage= "Name is Required")]
        [Remote("CheakName",null,ErrorMessage ="existssssss")]
        public string Name { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
