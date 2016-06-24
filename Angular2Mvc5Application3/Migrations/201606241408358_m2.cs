namespace Angular2Mvc5Application3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "json", c => c.String());
            AddColumn("dbo.Temps", "json", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Temps", "json");
            DropColumn("dbo.Posts", "json");
        }
    }
}
