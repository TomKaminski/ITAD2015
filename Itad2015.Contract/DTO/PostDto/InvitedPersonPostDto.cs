using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.PostDto
{
    public class InvitedPersonPostDto:PostBaseDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
