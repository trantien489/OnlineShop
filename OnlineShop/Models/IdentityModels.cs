using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace OnlineShop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public const string AdminRoleName = "Admin";
        public const string UserRoleName = "User";

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }


        public virtual ICollection<Invoice> Invoices { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        #region On Model Creating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<BaseModel>();

            modelBuilder.Entity<Invoice>().HasKey(invoice => invoice.Id)
                .HasRequired(invoice => invoice.ApplicationUser)
                .WithMany(applicationUser => applicationUser.Invoices)
                .Map(m => m.MapKey("UserId"));

            modelBuilder.Entity<InvoiceDetail>().HasKey(invoiceDetail => invoiceDetail.Id)
                .HasRequired(invoiceDetail => invoiceDetail.Invoice)
                .WithMany(invoice => invoice.InvoiceDetails)
                .HasForeignKey(invoiceDetail => invoiceDetail.InvoiceId);

            //modelbuilder.entity<invoicedetail>().hasmany(invoicedetail => invoicedetail.products)
            //    .withoptional().hasforeignkey(invoicedetail => invoicedetail.productid);

            modelBuilder.Entity<Product>().HasKey(product => product.Id)
                .HasRequired(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId);

            modelBuilder.Entity<Product>()
                .HasRequired(product => product.Producer)
                .WithMany(producer => producer.Products)
                .HasForeignKey(product => product.ProducerId);

            modelBuilder.Entity<Category>().HasKey(category => category.Id);


            modelBuilder.Entity<Producer>().HasKey(producer => producer.Id);


        }
        #endregion

        #region Save Changes
        public override int SaveChanges()
        {
            var entries = this.ChangeTracker.Entries<BaseModel>();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.ModifiedDate = DateTime.Now;
                        entry.Entity.Status = true;
                        break;
                    case EntityState.Modified:
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                }
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException != null)
                {
                    throw ex.InnerException.InnerException;
                }
                else
                {
                    throw ex.InnerException;
                }
            }
        }
        #endregion
    }
}