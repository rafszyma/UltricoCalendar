using Microsoft.EntityFrameworkCore;

namespace UltricoCalendarService.Persistance
{
    public class CalendarDbContext : DbContext
    { 
        public CalendarDbContext(DbContextOptions<CalendarDbContext> options)
            : base(options)
        {
        }
    }
}