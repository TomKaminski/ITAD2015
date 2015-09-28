using System;
using System.Collections.Generic;
using Itad2015.Model.Common;

namespace Itad2015.Model.Concrete
{
    public class Workshop:Entity
    {
        public Workshop()
        {
            Guests = new HashSet<Guest>();
        }

        public string Title { get; set; }
        public int MaxParticipants { get; set; }
        public string TutorName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }

        public virtual HashSet<Guest> Guests { get; set; }
    }
}
