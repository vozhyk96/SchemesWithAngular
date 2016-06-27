namespace Angular2Mvc5Application3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VoteLogs", "SectionId", c => c.Short(nullable: false));
            AddColumn("dbo.VoteLogs", "VoteForId", c => c.Int(nullable: false));
            AddColumn("dbo.VoteLogs", "UserName", c => c.String());
            AddColumn("dbo.VoteLogs", "Vote", c => c.Short(nullable: false));
            AddColumn("dbo.VoteLogs", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VoteLogs", "Active");
            DropColumn("dbo.VoteLogs", "Vote");
            DropColumn("dbo.VoteLogs", "UserName");
            DropColumn("dbo.VoteLogs", "VoteForId");
            DropColumn("dbo.VoteLogs", "SectionId");
        }
    }
}
