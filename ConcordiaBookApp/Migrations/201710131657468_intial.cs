namespace ConcordiaBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Authors", "BookRental_BookId", "dbo.BookRentals");
            DropIndex("dbo.Authors", new[] { "BookRental_BookId" });
            DropPrimaryKey("dbo.BookRentals");
            DropColumn("dbo.Authors", "BookRental_BookId");
            DropColumn("dbo.BookRentals", "BookId");
            DropColumn("dbo.BookRentals", "Title");
            DropColumn("dbo.BookRentals", "Description");
            DropColumn("dbo.BookRentals", "Version");
            DropColumn("dbo.BookRentals", "ISBN");
            DropColumn("dbo.BookRentals", "Genre");
            DropColumn("dbo.BookRentals", "RentingPrice");
            AddColumn("dbo.BookRentals", "BookRentalId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BookRentals", "BookRentalId");

        }
        public override void Down()
        {
            AddColumn("dbo.BookRentals", "RentingPrice", c => c.Double(nullable: false));
            AddColumn("dbo.BookRentals", "Genre", c => c.String());
            AddColumn("dbo.BookRentals", "ISBN", c => c.Int(nullable: false));
            AddColumn("dbo.BookRentals", "Version", c => c.Double(nullable: false));
            AddColumn("dbo.BookRentals", "Description", c => c.String());
            AddColumn("dbo.BookRentals", "Title", c => c.String());
            AddColumn("dbo.BookRentals", "BookId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Authors", "BookRental_BookId", c => c.Int());
            DropPrimaryKey("dbo.BookRentals");
            DropColumn("dbo.BookRentals", "BookRentalId");
            AddPrimaryKey("dbo.BookRentals", "BookId");
            CreateIndex("dbo.Authors", "BookRental_BookId");
            AddForeignKey("dbo.Authors", "BookRental_BookId", "dbo.BookRentals", "BookId");
        }
    }
}
