namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinformationinProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Information", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Information");
        }
    }
}
