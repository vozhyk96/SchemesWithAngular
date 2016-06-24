namespace Angular2Mvc5Application3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Temps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Posts", "image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "image");
            DropTable("dbo.Temps");
        }
    }
}
