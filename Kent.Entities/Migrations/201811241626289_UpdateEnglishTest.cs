namespace Kent.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEnglishTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionTemplates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        Type = c.Int(nullable: false),
                        HasBoxListingAnswer = c.Boolean(nullable: false),
                        RecordOrder = c.Int(nullable: false),
                        RecordActive = c.Boolean(nullable: false),
                        RecordDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastUpdateBy = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.QuestionSections", "ListAnwser", c => c.String());
            AddColumn("dbo.QuestionSections", "HasTemplate", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuestionSections", "ContainListAnswers", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuestionSections", "QuestionTemplateID", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "QuestionNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "QuestionString", c => c.String());
            AddColumn("dbo.Questions", "Answer", c => c.String());
            CreateIndex("dbo.QuestionSections", "QuestionTemplateID");
            AddForeignKey("dbo.QuestionSections", "QuestionTemplateID", "dbo.QuestionTemplates", "ID");
            DropColumn("dbo.Questions", "Questions");
            DropColumn("dbo.Questions", "Answers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Answers", c => c.String());
            AddColumn("dbo.Questions", "Questions", c => c.String());
            DropForeignKey("dbo.QuestionSections", "QuestionTemplateID", "dbo.QuestionTemplates");
            DropIndex("dbo.QuestionSections", new[] { "QuestionTemplateID" });
            DropColumn("dbo.Questions", "Answer");
            DropColumn("dbo.Questions", "QuestionString");
            DropColumn("dbo.Questions", "QuestionNumber");
            DropColumn("dbo.QuestionSections", "QuestionTemplateID");
            DropColumn("dbo.QuestionSections", "ContainListAnswers");
            DropColumn("dbo.QuestionSections", "HasTemplate");
            DropColumn("dbo.QuestionSections", "ListAnwser");
            DropTable("dbo.QuestionTemplates");
        }
    }
}
