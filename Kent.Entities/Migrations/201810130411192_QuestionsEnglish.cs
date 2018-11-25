namespace Kent.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionsEnglish : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionKits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
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
                "dbo.QuestionSections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SectionType = c.Int(nullable: false),
                        Description = c.String(),
                        QuestionKitID = c.Int(nullable: false),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuestionKits", t => t.QuestionKitID)
                .Index(t => t.QuestionKitID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Questions = c.String(),
                        Answers = c.String(),
                        SelectedAnswers = c.String(),
                        SectionID = c.Int(nullable: false),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuestionSections", t => t.SectionID)
                .Index(t => t.SectionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "SectionID", "dbo.QuestionSections");
            DropForeignKey("dbo.QuestionSections", "QuestionKitID", "dbo.QuestionKits");
            DropIndex("dbo.Questions", new[] { "SectionID" });
            DropIndex("dbo.QuestionSections", new[] { "QuestionKitID" });
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionSections");
            DropTable("dbo.QuestionKits");
        }
    }
}
