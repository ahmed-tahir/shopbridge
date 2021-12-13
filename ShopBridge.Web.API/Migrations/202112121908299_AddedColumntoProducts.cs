namespace ShopBridge.Web.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumntoProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UpdatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UpdatedDate");
        }
    }
}
