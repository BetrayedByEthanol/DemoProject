using DemoProject.Backend.ModelDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoProject.Backend.Data
{
    public class ApplicationUserStore : UserStore<UserIdentity>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
