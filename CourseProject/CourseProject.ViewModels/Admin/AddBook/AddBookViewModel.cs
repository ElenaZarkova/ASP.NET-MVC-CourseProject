using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.ViewModels.Admin.AddBook
{
    public class AddBookViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [AllowHtml]
        public string Title { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(80)]
        [AllowHtml]
        public string Author { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        [AllowHtml]
        public string Description { get; set; }

        // TODO: Fix date validation
        [Required]
        [Display(Name = "Published on")]
        [Range(typeof(DateTime), "1/1/1400", "1/1/3000")]
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