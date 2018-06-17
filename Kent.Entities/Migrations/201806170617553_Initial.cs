namespace Kent.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.EmailQueues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Subject = c.String(),
                        From = c.String(),
                        FromName = c.String(),
                        To = c.String(),
                        ToName = c.String(),
                        CC = c.String(),
                        BCC = c.String(),
                        Body = c.String(),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                        Form_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Forms", t => t.Form_ID)
                .Index(t => t.Form_ID);
            
            CreateTable(
                "dbo.EmailTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Template = c.String(),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FormTypeID = c.Int(nullable: false),
                        Data = c.String(),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Salers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SalerName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserTypeId = c.Int(nullable: false),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        LastLogin = c.DateTime(),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserTypeName = c.String(),
                        Level = c.Int(nullable: false),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.EmailQueues", "Form_ID", "dbo.Forms");
            DropForeignKey("dbo.EmailLogs", "EmailQueueID", "dbo.EmailQueues");
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.EmailQueues", new[] { "Form_ID" });
            DropIndex("dbo.EmailLogs", new[] { "EmailQueueID" });
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
            DropTable("dbo.Salers");
            DropTable("dbo.Forms");
            DropTable("dbo.EmailTypes");
            DropTable("dbo.EmailQueues");
            DropTable("dbo.EmailLogs");
        }
    }
}
