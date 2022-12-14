namespace TodoApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Username = c.String(nullable: false, maxLength: 255),
                    HashedPassword = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true);

            CreateTable(
                "dbo.Todoes",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Summary = c.String(nullable: false, maxLength: 255),
                    Detail = c.String(maxLength: 1000),
                    DeadLine = c.DateTime(),
                    Done = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserRoles",
                c => new
                {
                    User_Id = c.Guid(nullable: false),
                    Role_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Roles", new[] { "Name" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Todoes");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
