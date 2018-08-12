namespace Kent.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MultiLanguage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FooterTemplates", "ContentEnglish", c => c.String());
            AddColumn("dbo.HeaderTemplates", "ContentEnglish", c => c.String());
            AddColumn("dbo.Pages", "TitleEnglish", c => c.String(maxLength: 255));
            AddColumn("dbo.Pages", "ContentEnglish", c => c.String());
            AddColumn("dbo.Pages", "FriendlyUrlEnglish", c => c.String(maxLength: 255));
            DropColumn("dbo.Pages", "ContentWorking");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "ContentWorking", c => c.String());
            DropColumn("dbo.Pages", "FriendlyUrlEnglish");
            DropColumn("dbo.Pages", "ContentEnglish");
            DropColumn("dbo.Pages", "TitleEnglish");
            DropColumn("dbo.HeaderTemplates", "ContentEnglish");
            DropColumn("dbo.FooterTemplates", "ContentEnglish");
        }
    }
}
