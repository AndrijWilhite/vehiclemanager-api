namespace VehicleManager.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ColorID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ColorID);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        VehicleTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.VehicleTypeID);
            
            AddColumn("dbo.Vehicles", "ColorID", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "VehicleTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "Vehicle_VehicleID", c => c.Int());
            CreateIndex("dbo.Vehicles", "ColorID");
            CreateIndex("dbo.Vehicles", "Vehicle_VehicleID");
            AddForeignKey("dbo.Vehicles", "ColorID", "dbo.Colors", "ColorID", cascadeDelete: true);
            AddForeignKey("dbo.Vehicles", "Vehicle_VehicleID", "dbo.Vehicles", "VehicleID");
            AddForeignKey("dbo.Vehicles", "ColorID", "dbo.VehicleTypes", "VehicleTypeID", cascadeDelete: true);
            DropColumn("dbo.Vehicles", "Color");
            DropColumn("dbo.Vehicles", "VehicleType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "VehicleType", c => c.String());
            AddColumn("dbo.Vehicles", "Color", c => c.String());
            DropForeignKey("dbo.Vehicles", "ColorID", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "Vehicle_VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "ColorID", "dbo.Colors");
            DropIndex("dbo.Vehicles", new[] { "Vehicle_VehicleID" });
            DropIndex("dbo.Vehicles", new[] { "ColorID" });
            DropColumn("dbo.Vehicles", "Vehicle_VehicleID");
            DropColumn("dbo.Vehicles", "VehicleTypeID");
            DropColumn("dbo.Vehicles", "ColorID");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Colors");
        }
    }
}
