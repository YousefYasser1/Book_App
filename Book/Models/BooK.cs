using System.ComponentModel.DataAnnotations;

namespace Book.Models
{
    public class BooK
    {
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Rate { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public byte CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
