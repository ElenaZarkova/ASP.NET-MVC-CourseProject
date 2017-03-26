using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Value { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
