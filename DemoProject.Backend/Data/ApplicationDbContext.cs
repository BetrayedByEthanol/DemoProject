using DemoProject.Backend.ModelDTOs;
using DemoProject.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DemoProject.Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BugModel> Bugs { get; set; }
        public DbSet<FeatureTicketModel> FeatureTickets { get; set; }
        public DbSet<FeatureModel> Features { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<StoryModel> UserStories { get; set; }
        public DbSet<UserIdentity> uid { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(model =>
            {
                model.Property(prop => prop.Email).HasConversion(mail => mail.Address, mail => new MailAddress(mail));
                model.Property(prop => prop.UserName).HasMaxLength(32);
                model.Property(prop => prop.Role).HasConversion(role => role.Name, role => new IdentityRole(role)).HasMaxLength(32);
            });

            modelBuilder.Entity<FeatureTicketModel>(model =>
            {
                model.Property(prop => prop.name).HasMaxLength(32);
                model.Property(prop => prop.description).HasMaxLength(256);
            });

            modelBuilder.Entity<BugModel>(model =>
            {
                model.Property(prop => prop.name).HasMaxLength(32);
                model.Property(prop => prop.description).HasMaxLength(256);
            });
        }
    }
}
