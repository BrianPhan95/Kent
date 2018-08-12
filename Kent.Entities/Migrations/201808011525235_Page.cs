namespace Kent.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Page : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FooterTemplates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.String(),
                        IsDefaultTemplate = c.Boolean(nullable: false),
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
                "dbo.HeaderTemplates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.String(),
                        IsDefaultTemplate = c.Boolean(nullable: false),
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
                "dbo.Pages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Content = c.String(),
                        ContentWorking = c.String(),
                        FriendlyUrl = c.String(maxLength: 255),
                        Keywords = c.String(maxLength: 255),
                        HeaderTemplateId = c.Int(),
                        FooterTemplateId = c.Int(),
                        Status = c.Int(nullable: false),
                        IsHomePage = c.Boolean(nullable: false),
                        IncludeInSiteNavigation = c.Boolean(nullable: false),
                        StartPublishingDate = c.DateTime(),
                        EndPublishingDate = c.DateTime(),
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
                .ForeignKey("dbo.FooterTemplates", t => t.FooterTemplateId)
                .ForeignKey("dbo.HeaderTemplates", t => t.HeaderTemplateId)
                .ForeignKey("dbo.Pages", t => t.ParentId)
                .Index(t => t.HeaderTemplateId)
                .Index(t => t.FooterTemplateId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pages", "ParentId", "dbo.Pages");
            DropForeignKey("dbo.Pages", "HeaderTemplateId", "dbo.HeaderTemplates");
            DropForeignKey("dbo.Pages", "FooterTemplateId", "dbo.FooterTemplates");
            DropIndex("dbo.Pages", new[] { "ParentId" });
            DropIndex("dbo.Pages", new[] { "FooterTemplateId" });
            DropIndex("dbo.Pages", new[] { "HeaderTemplateId" });
            DropTable("dbo.Pages");
            DropTable("dbo.HeaderTemplates");
            DropTable("dbo.FooterTemplates");
        }
    }
}
