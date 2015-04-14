namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        DATE = c.DateTime(nullable: false),
                        LEVEL = c.String(),
                        Message = c.String(),
                        EXCEPTION = c.String(),
                    })
                .PrimaryKey(t => t.DATE);
            
            CreateTable(
                "dbo.FULL_TIME_EMP",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        NAME = c.String(maxLength: 255),
                        START_DATE = c.DateTime(nullable: false),
                        SALARY = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PART_TIME_EMP",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        NAME = c.String(maxLength: 255),
                        START_DATE = c.DateTime(nullable: false),
                        RATE = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PART_TIME_EMP");
            DropTable("dbo.FULL_TIME_EMP");
            DropTable("dbo.Log");
        }
    }
}
