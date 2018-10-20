namespace StaffPositions.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BasketId = c.String(maxLength: 128),
                        ProductId = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketId)
                .Index(t => t.BasketId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeveloperPositions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Position = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        DeveloperId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        FullName = c.String(),
                        Email = c.String(nullable: false),
                        Position = c.String(),
                        Photo = c.String(),
                        SuperiorID = c.Int(),
                        SuperiorName = c.String(),
                        Id = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.DeveloperId)
                .ForeignKey("dbo.Developers", t => t.SuperiorID)
                .Index(t => t.SuperiorID);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Category = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 20),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(),
                        Image = c.String(),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developers", "SuperiorID", "dbo.Developers");
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.Developers", new[] { "SuperiorID" });
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Developers");
            DropTable("dbo.DeveloperPositions");
            DropTable("dbo.Customers");
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
        }
    }
}
