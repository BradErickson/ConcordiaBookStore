namespace ConcordiaBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookstore : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Authors", "BooksInStore_BookId", "dbo.BooksInStores");
            DropIndex("dbo.Authors", new[] { "BooksInStore_BookId" });
            DropPrimaryKey("dbo.BooksInStores");
            DropColumn("dbo.Authors", "BooksInStore_BookId");
            DropColumn("dbo.Books", "BookSellerId");
            DropColumn("dbo.BooksInStores", "BookId");
            DropColumn("dbo.BooksInStores", "Title");
            DropColumn("dbo.BooksInStores", "Description");
            DropColumn("dbo.BooksInStores", "Version");
            DropColumn("dbo.BooksInStores", "ISBN");
            DropColumn("dbo.BooksInStores", "Quantity");
            DropColumn("dbo.BooksInStores", "Genre");
            DropColumn("dbo.BooksInStores", "SellingPrice");
            DropColumn("dbo.BooksInStores", "RentingPrice");
            DropColumn("dbo.BooksInStores", "AvailableTrade");
            DropColumn("dbo.BooksInStores", "PhotoUrl");
            AddColumn("dbo.BooksInStores", "BookInStoreId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.BooksInStores", "Book_BookId", c => c.Int());
            AddPrimaryKey("dbo.BooksInStores", "BookInStoreId");
            CreateIndex("dbo.BooksInStores", "Book_BookId");
            AddForeignKey("dbo.BooksInStores", "Book_BookId", "dbo.Books", "BookId");

        }
        
        public override void Down()
        {
            AddColumn("dbo.BooksInStores", "PhotoUrl", c => c.String());
            AddColumn("dbo.BooksInStores", "AvailableTrade", c => c.Boolean(nullable: false));
            AddColumn("dbo.BooksInStores", "RentingPrice", c => c.Double(nullable: false));
            AddColumn("dbo.BooksInStores", "SellingPrice", c => c.Double(nullable: false));
            AddColumn("dbo.BooksInStores", "Genre", c => c.String());
            AddColumn("dbo.BooksInStores", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.BooksInStores", "ISBN", c => c.Int(nullable: false));
            AddColumn("dbo.BooksInStores", "Version", c => c.Double(nullable: false));
            AddColumn("dbo.BooksInStores", "Description", c => c.String());
            AddColumn("dbo.BooksInStores", "Title", c => c.String());
            AddColumn("dbo.BooksInStores", "BookId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Books", "BookSellerId", c => c.String());
            AddColumn("dbo.Authors", "BooksInStore_BookId", c => c.Int());
            DropForeignKey("dbo.BooksInStores", "Book_BookId", "dbo.Books");
            DropIndex("dbo.BooksInStores", new[] { "Book_BookId" });
            DropPrimaryKey("dbo.BooksInStores");
            DropColumn("dbo.BooksInStores", "Book_BookId");
            DropColumn("dbo.BooksInStores", "BookInStoreId");
            AddPrimaryKey("dbo.BooksInStores", "BookId");
            CreateIndex("dbo.Authors", "BooksInStore_BookId");
            AddForeignKey("dbo.Authors", "BooksInStore_BookId", "dbo.BooksInStores", "BookId");
        }
    }
}
