using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace P7CreateRestApi.Data;

public class LocalDbContext : IdentityDbContext<User>
{
    public DbSet<BidList> BidLists { get; set; }
    public DbSet<CurvePoint> CurvePoints { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<RuleName> RuleNames { get; set; }
    public DbSet<Trade> Trades { get; set; }
        
    public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
    {
    }
}