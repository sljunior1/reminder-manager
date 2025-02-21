using Microsoft.EntityFrameworkCore;
using ReminderManagerApi.Models;

namespace ReminderManagerApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {    
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ReminderModel> Reminders { get; set; }
        public DbSet<AttachReminderModel> AttachReminders { get; set; }
    }   
}
