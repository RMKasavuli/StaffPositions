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
            
            AlterColumn("dbo.Developers", "FirstName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Developers", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Developers", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Developers", "Email", c => c.String());
            AlterColumn("dbo.Developers", "LastName", c => c.String(maxLength: 20));
            AlterColumn("dbo.Developers", "FirstName", c => c.String(maxLength: 20));
            DropTable("dbo.DeveloperPositions");
        }
    }
}
