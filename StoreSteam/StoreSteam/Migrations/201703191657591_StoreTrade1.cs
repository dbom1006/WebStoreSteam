namespace StoreSteam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreTrade1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actions", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.Tags", "Item_Id", "dbo.Items");
            DropIndex("dbo.Actions", new[] { "Item_Id" });
            DropIndex("dbo.Tags", new[] { "Item_Id" });
            DropTable("dbo.Actions");
            DropTable("dbo.Tags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        InternalName = c.String(),
                        Category = c.String(),
                        Color = c.String(),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Link = c.String(),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Tags", "Item_Id");
            CreateIndex("dbo.Actions", "Item_Id");
            AddForeignKey("dbo.Tags", "Item_Id", "dbo.Items", "Id");
            AddForeignKey("dbo.Actions", "Item_Id", "dbo.Items", "Id");
        }
    }
}
