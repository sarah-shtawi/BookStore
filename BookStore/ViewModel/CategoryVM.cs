using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class CategoryVM
    {

        public int Id { get; set; }
        [MaxLength(30,ErrorMessage="Max Langth 30 characters")]
        [Required(ErrorMessage= "Name is Required")]
        public string Name { get; set; } = null!;
    }
}
