using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Itad2015.Areas.Admin.ViewModels
{
    public class WorkshopCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int MaxParticipants { get; set; }
        [Required]
        public string TutorName { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public string Room { get; set; }

        [DataType(DataType.Upload)]
        [Required]
        public HttpPostedFileBase Image { get; set; }
    }

    public class WorkshopEditViewModel : WorkshopCreateViewModel
    {
        [Required]
        public int Id { get; set; }

        public string ImgPath { get; set; }
    }

    public class WorkshopListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxParticipants { get; set; }
        public string TutorName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public string Room { get; set; }
    }
}