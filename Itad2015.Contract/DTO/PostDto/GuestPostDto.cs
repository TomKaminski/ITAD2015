using System;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.PostDto
{
    public class GuestPostDto : PostBaseDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationTime { get; set; }
        public Size Size { get; set; }
        public bool QrEmailSent { get; set; }
        public string Info { get; set; }
        public bool ShirtOrdered { get; set; }
        public bool AgendaEmailSent { get; set; }
        public bool PolicyAccepted { get; set; }
    }
}
