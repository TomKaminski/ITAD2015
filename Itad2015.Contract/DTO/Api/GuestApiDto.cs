using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itad2015.Contract.DTO.Api
{
    public class GuestApiDtoPerson
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SchoolName { get; set; }
        public DateTime? CheckInDate { get; set; }
    }

    public class GuestApiDto
    {
        public GuestApiDtoPerson Person { get; set; }
        public bool Status { get; set; }
        public string Error { get; set; }
    }
}
