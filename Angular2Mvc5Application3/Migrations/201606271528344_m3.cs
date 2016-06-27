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
