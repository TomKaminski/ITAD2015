using System.Web.Helpers;
using Itad2015.Service.Helpers.Interfaces;

namespace Itad2015.Service.Helpers
{
    public class HashGenerator:IHashGenerator
    {
        public string CreateHash(string text)
        {
            return Crypto.SHA1(text + Crypto.GenerateSalt()).ToLower();
        }
    }
}
