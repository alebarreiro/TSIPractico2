namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FULL_TIME_EMP", "FIRST_LOGIN", c => c.Boolean(nullable: false));
            AddColumn("dbo.PART_TIME_EMP", "FIRST_LOGIN", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PART_TIME_EMP", "FIRST_LOGIN");
            DropColumn("dbo.FULL_TIME_EMP", "FIRST_LOGIN");
        }
    }
}
