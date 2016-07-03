namespace Angular2Mvc5Application3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m91 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "LikedUserIds", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "LikedUserIds");
        }
    }
}
