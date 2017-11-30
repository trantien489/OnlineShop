namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateForeignKeyProductInvoiceDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "InvoiceDetail_Id", "dbo.InvoiceDetails");
            DropForeignKey("dbo.InvoiceDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.InvoiceDetails", "Product_Id", "dbo.Products");
            DropIndex("dbo.InvoiceDetails", new[] { "ProductId" });
            DropIndex("dbo.InvoiceDetails", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "InvoiceDetail_Id" });
            DropColumn("dbo.InvoiceDetails", "ProductId");
            RenameColumn(table: "dbo.InvoiceDetails", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.InvoiceDetails", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceDetails", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoiceDetails", "ProductId");
            AddForeignKey("dbo.InvoiceDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "InvoiceDetail_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "InvoiceDetail_Id", c => c.Int());
            DropForeignKey("dbo.InvoiceDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.InvoiceDetails", new[] { "ProductId" });
            AlterColumn("dbo.InvoiceDetails", "ProductId", c => c.Int());
            AlterColumn("dbo.InvoiceDetails", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.InvoiceDetails", name: "ProductId", newName: "Product_Id");
            AddColumn("dbo.InvoiceDetails", "ProductId", c => c.Int());
            CreateIndex("dbo.Products", "InvoiceDetail_Id");
            CreateIndex("dbo.InvoiceDetails", "Product_Id");
            CreateIndex("dbo.InvoiceDetails", "ProductId");
            AddForeignKey("dbo.InvoiceDetails", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.InvoiceDetails", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Products", "InvoiceDetail_Id", "dbo.InvoiceDetails", "Id");
        }
    }
}
