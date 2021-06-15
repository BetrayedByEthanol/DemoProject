using Microsoft.AspNetCore.Identity;

namespace DemoProject.Backend.Security
{
    class LookUpNormalizer : ILookupNormalizer
    {
        public string NormalizeEmail(string email)
        {
            return email;
        }

        public string NormalizeName(string name)
        {
            return name;
        }
    }
}
