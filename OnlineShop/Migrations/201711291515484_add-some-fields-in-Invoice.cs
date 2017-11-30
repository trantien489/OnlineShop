namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsomefieldsinInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "NameReceive", c => c.String());
            AddColumn("dbo.Invoices", "PhoneReceive", c => c.String());
            AddColumn("dbo.Invoices", "AddressReceive", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "AddressReceive");
            DropColumn("dbo.Invoices", "PhoneReceive");
            DropColumn("dbo.Invoices", "NameReceive");
        }
    }
}
