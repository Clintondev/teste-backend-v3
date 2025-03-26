using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TheatricalPlayersRefactoringKata.Persistence; 

public class TheaterContextFactory : IDesignTimeDbContextFactory<TheaterContext>
{
    public TheaterContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TheaterContext>();

        optionsBuilder.UseSqlite("Data Source=theater.db");

        return new TheaterContext(optionsBuilder.Options);
    }
}
