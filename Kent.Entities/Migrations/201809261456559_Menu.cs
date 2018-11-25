namespace Kent.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        Area = c.String(),
                        Visible = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        Hierarchy = c.String(),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menus", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menus", "ParentId", "dbo.Menus");
            DropIndex("dbo.Menus", new[] { "ParentId" });
            DropTable("dbo.Menus");
        }
    }
}
