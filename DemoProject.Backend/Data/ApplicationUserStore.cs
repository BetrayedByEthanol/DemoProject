using DemoProject.Backend.ModelDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DemoProject.Backend.Data
{
    public class ApplicationUserStore : UserStore<UserIdentity>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
