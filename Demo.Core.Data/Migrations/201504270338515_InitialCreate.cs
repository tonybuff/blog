namespace Demo.Core.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        PassWord = c.String(nullable: false, maxLength: 50),
                        UnickName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Salt = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        TimeSpan = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        UserRole_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRole", t => t.UserRole_Id)
                .Index(t => t.UserRole_Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        RoleType = c.Int(nullable: false),
                        RoleTypeNum = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        TimeSpan = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoginLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IpAddress = c.String(nullable: false, maxLength: 15),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        TimeSpan = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Member_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Member", t => t.Member_Id)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.ConsumeRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConsumeType = c.String(),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        TimeSpan = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.LoginLogs", new[] { "Member_Id" });
            DropIndex("dbo.Member", new[] { "UserRole_Id" });
            DropForeignKey("dbo.LoginLogs", "Member_Id", "dbo.Member");
            DropForeignKey("dbo.Member", "UserRole_Id", "dbo.UserRole");
            DropTable("dbo.ConsumeRecord");
            DropTable("dbo.LoginLogs");
            DropTable("dbo.UserRole");
            DropTable("dbo.Member");
        }
    }
}
