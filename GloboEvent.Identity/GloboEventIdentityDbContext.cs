using GloboEvent.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GloboEvent.Identity
{
    public class GloboEventIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public GloboEventIdentityDbContext(DbContextOptions<GloboEventIdentityDbContext> options) : base(options)
        {
        }
    }
}
