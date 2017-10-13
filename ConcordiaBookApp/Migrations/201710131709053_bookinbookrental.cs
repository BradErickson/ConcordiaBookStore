namespace ConcordiaBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookinbookrental : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookRentals", "RentedBook_BookId", c => c.Int());
            CreateIndex("dbo.BookRentals", "RentedBook_BookId");
            AddForeignKey("dbo.BookRentals", "RentedBook_BookId", "dbo.Books", "BookId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookRentals", "RentedBook_BookId", "dbo.Books");
            DropIndex("dbo.BookRentals", new[] { "RentedBook_BookId" });
            DropColumn("dbo.BookRentals", "RentedBook_BookId");
        }
    }
}
