namespace ConcordiaBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "BookSellerId_UserId", "dbo.UserProfiles");
            DropIndex("dbo.Books", new[] { "BookSellerId_UserId" });
            AddColumn("dbo.Books", "BookSellerId", c => c.String());
            DropColumn("dbo.Books", "BookSellerId_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "BookSellerId_UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Books", "BookSellerId");
            CreateIndex("dbo.Books", "BookSellerId_UserId");
            AddForeignKey("dbo.Books", "BookSellerId_UserId", "dbo.UserProfiles", "UserId");
        }
    }
}
