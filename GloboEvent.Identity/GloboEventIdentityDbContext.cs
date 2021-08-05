using GloboEvent.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Identity
{
    public class GloboEventIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public GloboEventIdentityDbContext(DbContextOptions<GloboEventIdentityDbContext> options) : base(options)
        {
        }
    }
}
