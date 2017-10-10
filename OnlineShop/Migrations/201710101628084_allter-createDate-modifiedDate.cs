namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alltercreateDatemodifiedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Categories", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.CategoryProducers", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.CategoryProducers", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Producers", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Producers", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Products", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Products", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.InvoiceDetails", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.InvoiceDetails", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Invoices", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Invoices", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Invoices", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.InvoiceDetails", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.InvoiceDetails", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Producers", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Producers", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CategoryProducers", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CategoryProducers", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
