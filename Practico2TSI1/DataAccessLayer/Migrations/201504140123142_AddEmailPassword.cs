namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FULL_TIME_EMP", "EMAIL", c => c.String());
            AddColumn("dbo.FULL_TIME_EMP", "PASSWORD", c => c.String());
            AddColumn("dbo.PART_TIME_EMP", "EMAIL", c => c.String());
            AddColumn("dbo.PART_TIME_EMP", "PASSWORD", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PART_TIME_EMP", "PASSWORD");
            DropColumn("dbo.PART_TIME_EMP", "EMAIL");
            DropColumn("dbo.FULL_TIME_EMP", "PASSWORD");
            DropColumn("dbo.FULL_TIME_EMP", "EMAIL");
        }
    }
}
