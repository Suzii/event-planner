namespace EventPlanner.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "OrganizerId", "dbo.Users");
            DropForeignKey("dbo.Places", "EventEntity_Id", "dbo.Events");
            DropForeignKey("dbo.VotesForDate", "UserId", "dbo.Users");
            DropForeignKey("dbo.VotesForPlace", "UserId", "dbo.Users");
            DropIndex("dbo.Events", new[] { "OrganizerId" });
            DropIndex("dbo.VotesForDate", new[] { "UserId" });
            DropIndex("dbo.VotesForPlace", new[] { "UserId" });
            DropIndex("dbo.Places", new[] { "EventEntity_Id" });
            DropColumn("dbo.Places", "EventId");
            RenameColumn(table: "dbo.Places", name: "EventEntity_Id", newName: "EventId");
            AlterColumn("dbo.Events", "OrganizerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.VotesForDate", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.VotesForPlace", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Places", "EventId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Events", "OrganizerId");
            CreateIndex("dbo.VotesForDate", "UserId");
            CreateIndex("dbo.VotesForPlace", "UserId");
            CreateIndex("dbo.Places", "EventId");
            AddForeignKey("dbo.Events", "OrganizerId", "dbo.Users", "Id");
            AddForeignKey("dbo.Places", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VotesForDate", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.VotesForPlace", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.TimeSlots", "EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeSlots", "EventId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.VotesForPlace", "UserId", "dbo.Users");
            DropForeignKey("dbo.VotesForDate", "UserId", "dbo.Users");
            DropForeignKey("dbo.Places", "EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "OrganizerId", "dbo.Users");
            DropIndex("dbo.Places", new[] { "EventId" });
            DropIndex("dbo.VotesForPlace", new[] { "UserId" });
            DropIndex("dbo.VotesForDate", new[] { "UserId" });
            DropIndex("dbo.Events", new[] { "OrganizerId" });
            AlterColumn("dbo.Places", "EventId", c => c.Guid());
            AlterColumn("dbo.VotesForPlace", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.VotesForDate", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Events", "OrganizerId", c => c.String(nullable: false, maxLength: 128));
            RenameColumn(table: "dbo.Places", name: "EventId", newName: "EventEntity_Id");
            AddColumn("dbo.Places", "EventId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Places", "EventEntity_Id");
            CreateIndex("dbo.VotesForPlace", "UserId");
            CreateIndex("dbo.VotesForDate", "UserId");
            CreateIndex("dbo.Events", "OrganizerId");
            AddForeignKey("dbo.VotesForPlace", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VotesForDate", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Places", "EventEntity_Id", "dbo.Events", "Id");
            AddForeignKey("dbo.Events", "OrganizerId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
