namespace StaffPositions.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developers", "SuperiorID", "dbo.Developers");
            DropIndex("dbo.Developers", new[] { "SuperiorID" });
            DropTable("dbo.Developers");
            DropTable("dbo.DeveloperPositions");
        }
    }
}
