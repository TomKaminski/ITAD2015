using System;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.PostDto
{
    public class WorkshopPostDto : PostBaseDto
    {
        public string Title { get; set; }
        public int MaxParticipants { get; set; }
        public string TutorName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
    }
}
