using Microsoft.EntityFrameworkCore;

namespace LifestyleSurvey.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SurveyResponse> SurveyResponses { get; set; }
    }
}