namespace Elmah.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ElmahError",
                c => new
                    {
                        ErrorId = c.Guid(nullable: false),
                        Application = c.String(nullable: false, maxLength: 60),
                        Host = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 100),
                        Source = c.String(nullable: false, maxLength: 60),
                        Message = c.String(nullable: false, maxLength: 500),
                        User = c.String(maxLength: 50),
                        StatusCode = c.Int(nullable: false),
                        TimeUtc = c.DateTime(nullable: false),
                        Sequence = c.Int(nullable: false, identity: true),
                        AllXml = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.ErrorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ElmahError");
        }
    }
}
