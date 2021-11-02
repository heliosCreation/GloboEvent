using GloboEvent.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GloboEvent.Identity
{
    public class GloboEventIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public GloboEventIdentityDbContext(DbContextOptions<GloboEventIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
