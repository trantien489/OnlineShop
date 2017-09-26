using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
    }
}