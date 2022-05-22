using Microsoft.EntityFrameworkCore;
using SnjMedical.ScheduleService.Models;

namespace SnjMedical.ScheduleService.DataContext;

public class DataContext : DbContext
{
    public DataContext()
    { }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }
    
    public DbSet<Schedule> Schedules { get; set; }
}