using CustomerServiceSystem.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsutomerServiceSystem.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=CustomerServiceSystem;integrated security=true;");
        }

        /*public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }*/

        public DbSet<Registration> Registration { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Statu> Statu { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<PasswordCode> PasswordCode { get; set; }
    }
}
