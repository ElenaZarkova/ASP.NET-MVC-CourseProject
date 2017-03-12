using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Web.Models
{
    public class AddBookViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Title { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(80)]
        public string Author { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Description { get; set; }

        // TODO: Fix date validation
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Published on")]
        public DateTime PublishedOn { get; set; }

        [Required]
        [Display(Name = "Cover photo")]
        public HttpPostedFileBase CoverFile { get; set; }
        
        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        // TODO: Genres in extended model
        public IEnumerable<SelectListItem> Genres { get; set; }
    }
}