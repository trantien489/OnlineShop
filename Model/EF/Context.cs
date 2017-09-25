using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Context: IdentityDbContext<ApplicationUser>
    {
        public Context()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static Context Create()
        {
            return new Context();
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
                .HasRequired(invoiceDetail => invoiceDetail.Invoice )
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
