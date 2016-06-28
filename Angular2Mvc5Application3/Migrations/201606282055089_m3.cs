namespace Angular2Mvc5Application3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VoteLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        SectionId = c.Short(nullable: false),
                        VoteForId = c.Int(nullable: false),
                        UserName = c.String(),
                        Vote = c.Short(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Posts", "Votes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Votes");
            DropTable("dbo.VoteLogs");
        }
    }
}
