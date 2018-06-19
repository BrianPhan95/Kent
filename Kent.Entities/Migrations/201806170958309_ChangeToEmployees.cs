namespace Kent.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToEmployees : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Salers", newName: "Employees");
            AddColumn("dbo.Employees", "Name", c => c.String());
            DropColumn("dbo.Employees", "SalerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "SalerName", c => c.String());
            DropColumn("dbo.Employees", "Name");
            RenameTable(name: "dbo.Employees", newName: "Salers");
        }
    }
}
