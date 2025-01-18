using Lab2_LeVoKhanhMinh.Models;
using Microsoft.EntityFrameworkCore;
namespace Lab2_LeVoKhanhMinh.Data;


public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<DeviceCategory> DeviceCategories { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<User> Users { get; set; }
     
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         modelBuilder.Entity<Device>()
             .HasOne(d => d.DeviceCategory)  // Each Device has one DeviceCategory
             .WithMany(c => c.Devices)       // Each DeviceCategory has many Devices
             .HasForeignKey(d => d.DeviceCategoryId);  // Reference the foreign key property, not d.DeviceCategory.Id



         base.OnModelCreating(modelBuilder);
     }
}