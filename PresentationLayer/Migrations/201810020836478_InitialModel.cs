namespace LanguageProcessor.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(),
                    Time = c.DateTime(nullable: false),
                    Location = c.String(),
                    HostName = c.String(),
                    IpAddress_Address = c.Long(nullable: false),
                    IpAddress_ScopeId = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
