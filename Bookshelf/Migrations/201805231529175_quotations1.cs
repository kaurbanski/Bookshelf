namespace Bookshelf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quotations1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quotations",
                c => new
                    {
                        QuotationId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        QuotationEntity_QuotationId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.QuotationId)
                .ForeignKey("dbo.Quotations", t => t.QuotationEntity_QuotationId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.QuotationEntity_QuotationId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Quotations", "QuotationEntity_QuotationId", "dbo.Quotations");
            DropIndex("dbo.Quotations", new[] { "User_Id" });
            DropIndex("dbo.Quotations", new[] { "QuotationEntity_QuotationId" });
            DropTable("dbo.Quotations");
        }
    }
}
