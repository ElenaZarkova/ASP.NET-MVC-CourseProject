using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class UserBook
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int BookId { get; set; }

        public virtual User User { get; set; }

        public virtual Book Book { get; set; }

        public BookStatus BookStatus { get; set; }
    }
}
