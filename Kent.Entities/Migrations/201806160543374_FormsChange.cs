namespace Kent.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormsChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Forms", "EmailQueueID", "dbo.EmailQueues");
            DropForeignKey("dbo.Forms", "FormTypeID", "dbo.FormTypes");
            DropIndex("dbo.Forms", new[] { "FormTypeID" });
            DropIndex("dbo.Forms", new[] { "EmailQueueID" });
            CreateTable(
                "dbo.EmailLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmailQueueID = c.Int(nullable: false),
                        Message = c.String(),
                        Status = c.Int(nullable: false),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmailQueues", t => t.EmailQueueID)
                .Index(t => t.EmailQueueID);
            
            DropColumn("dbo.Forms", "EmailQueueID");
            DropTable("dbo.FormTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FormTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Forms", "EmailQueueID", c => c.Int(nullable: false));
            DropForeignKey("dbo.EmailLogs", "EmailQueueID", "dbo.EmailQueues");
            DropIndex("dbo.EmailLogs", new[] { "EmailQueueID" });
            DropTable("dbo.EmailLogs");
            CreateIndex("dbo.Forms", "EmailQueueID");
            CreateIndex("dbo.Forms", "FormTypeID");
            AddForeignKey("dbo.Forms", "FormTypeID", "dbo.FormTypes", "ID");
            AddForeignKey("dbo.Forms", "EmailQueueID", "dbo.EmailQueues", "ID");
        }
    }
}
