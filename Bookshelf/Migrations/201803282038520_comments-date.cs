namespace Bookshelf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentsdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "AddedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "AddedDate");
        }
    }
}
