namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtableCategoryProducer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "ProducerId", "dbo.Producers");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "ProducerId" });
            CreateTable(
                "dbo.CategoryProducers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProducerId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .Index(t => t.ProducerId)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.Products", "CategoryProducerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryProducerId");
            AddForeignKey("dbo.Products", "CategoryProducerId", "dbo.CategoryProducers", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "CategoryId");
            DropColumn("dbo.Products", "ProducerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProducerId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "CategoryProducerId", "dbo.CategoryProducers");
            DropForeignKey("dbo.CategoryProducers", "ProducerId", "dbo.Producers");
            DropForeignKey("dbo.CategoryProducers", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryProducerId" });
            DropIndex("dbo.CategoryProducers", new[] { "CategoryId" });
            DropIndex("dbo.CategoryProducers", new[] { "ProducerId" });
            DropColumn("dbo.Products", "CategoryProducerId");
            DropTable("dbo.CategoryProducers");
            CreateIndex("dbo.Products", "ProducerId");
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "ProducerId", "dbo.Producers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
