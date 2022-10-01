using Microsoft.EntityFrameworkCore;

namespace Formel.API
{
    class FormulaContext : DbContext
    {
        public FormulaContext(DbContextOptions<FormulaContext> options)
            : base(options) { }
        public DbSet<Formula> Formulas => Set<Formula>();
    }
}
