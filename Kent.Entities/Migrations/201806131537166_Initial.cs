namespace Kent.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FormTypeID = c.Int(nullable: false),
                        EmailQueueID = c.Int(nullable: false),
                        Data = c.String(),
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
                .ForeignKey("dbo.FormTypes", t => t.FormTypeID)
                .Index(t => t.FormTypeID)
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
                        CC = c.String(),
                        BCC = c.String(),
                        Body = c.String(),
                        DataType = c.String(),
                        DateFormat = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                        EmailTypeId = c.Int(nullable: false),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmailTypes", t => t.EmailTypeId)
                .Index(t => t.EmailTypeId);
            
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
            DropForeignKey("dbo.Forms", "FormTypeID", "dbo.FormTypes");
            DropForeignKey("dbo.Forms", "EmailQueueID", "dbo.EmailQueues");
            DropForeignKey("dbo.EmailQueues", "EmailTypeId", "dbo.EmailTypes");
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.EmailQueues", new[] { "EmailTypeId" });
            DropIndex("dbo.Forms", new[] { "EmailQueueID" });
            DropIndex("dbo.Forms", new[] { "FormTypeID" });
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
            DropTable("dbo.FormTypes");
            DropTable("dbo.EmailTypes");
            DropTable("dbo.EmailQueues");
            DropTable("dbo.Forms");
        }
    }
}
