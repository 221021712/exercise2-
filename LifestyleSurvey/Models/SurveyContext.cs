using LifestyleSurvey.Models;
using Microsoft.EntityFrameworkCore;

namespace LifestyleSurvey.Data
{
    public class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> options) : base(options)
        {
        }

        public DbSet<SurveyResponse> SurveyResponses { get; set; }
    }
}