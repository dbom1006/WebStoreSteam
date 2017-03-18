namespace StoreSteam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreTrade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumSlots = c.Int(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        IsGood = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppId = c.Int(nullable: false),
                        ClassId = c.String(),
                        Name = c.String(),
                        IconUrl = c.String(),
                        Color = c.String(),
                        Type = c.String(),
                        Tradable = c.Boolean(nullable: false),
                        Marketable = c.Boolean(nullable: false),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.Actions", "Item_Id", "dbo.Items");
            DropIndex("dbo.Tags", new[] { "Item_Id" });
            DropIndex("dbo.Actions", new[] { "Item_Id" });
            DropTable("dbo.Tags");
            DropTable("dbo.Actions");
            DropTable("dbo.Items");
            DropTable("dbo.Inventories");
        }
    }
}
