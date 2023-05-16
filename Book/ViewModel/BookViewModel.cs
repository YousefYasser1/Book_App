using Book.Models;
using System.ComponentModel.DataAnnotations;

namespace Book.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Rate { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Display(Name ="Category Type")]
        public byte CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }   
    }
}
