namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryProducerId", "dbo.CategoryProducers");
            DropIndex("dbo.Products", new[] { "CategoryProducerId" });
            AddColumn("dbo.Products", "CategoryId", c => c.Int());
            AddColumn("dbo.Products", "ProducerId", c => c.Int());
            DropColumn("dbo.Products", "CategoryProducerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryProducerId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "ProducerId");
            DropColumn("dbo.Products", "CategoryId");
            CreateIndex("dbo.Products", "CategoryProducerId");
            AddForeignKey("dbo.Products", "CategoryProducerId", "dbo.CategoryProducers", "Id", cascadeDelete: true);
        }
    }
}
