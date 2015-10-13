using Itad2015.Model.Common;

namespace Itad2015.Model.Concrete
{
    public class User:Entity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool SuperAdmin { get; set; }
    }
}
