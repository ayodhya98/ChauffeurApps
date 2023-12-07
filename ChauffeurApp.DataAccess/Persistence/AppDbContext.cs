using ChauffeurApp.Core.Common;
using ChauffeurApp.Core.Entities;
using ChauffeurApp.Shared.Services;
using ChauffeurApp.Shared.Services.IServices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChauffeurApp.DataAccess.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, long>
    {
        private readonly IClaimService _claimService;
        public AppDbContext(DbContextOptions options, IClaimService claimService) : base(options) 
        {
            _claimService = claimService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = Convert.ToInt64(_claimService.GetUserId());
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedById = Convert.ToInt64(_claimService.GetUserId());
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
   

}
