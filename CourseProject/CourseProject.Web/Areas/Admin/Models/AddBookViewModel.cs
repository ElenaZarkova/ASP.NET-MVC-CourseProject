using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Web.Areas.Admin.Models
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
        [Display(Name = "Published on")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishedOn { get; set; }

        [Required]
        [Display(Name = "Cover photo")]
        public HttpPostedFileBase CoverFile { get; set; }
        
        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        
        public IEnumerable<SelectListItem> Genres { get; set; }
    }
}