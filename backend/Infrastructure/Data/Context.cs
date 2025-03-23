using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public partial class Context : DbContext
    {
        public Context() { }

        public Context(DbContextOptions<Context> options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Locker> Lockers { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
        public virtual DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }

}
