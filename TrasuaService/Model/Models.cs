using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TrasuaService.Model
{
    public partial class Models : DbContext
    {
        public Models()
            : base("name=Models")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<OrderOnline> OrderOnlines { get; set; }
        public virtual DbSet<OrderOnlineDetail> OrderOnlineDetails { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }
        public virtual DbSet<TypeDrink> TypeDrinks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Bills)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeCreate);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OrderOnlines)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeAccept);

            modelBuilder.Entity<OrderOnline>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Session>()
                .Property(e => e.SessionID)
                .IsUnicode(false);

            modelBuilder.Entity<TypeDrink>()
                .HasMany(e => e.Drinks)
                .WithOptional(e => e.TypeDrink)
                .HasForeignKey(e => e.Type);
        }
    }
}
