using System;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.GetDto
{
    public class WorkshopGetDto : GetBaseDto
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
