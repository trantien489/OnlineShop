namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsomefieldsinInvoicev2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "EmailReceive", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "EmailReceive");
        }
    }
}
