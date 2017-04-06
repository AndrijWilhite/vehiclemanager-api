namespace VehicleManager.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
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
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Int(nullable: false, identity: true),
                        ColorID = c.Int(nullable: false),
                        VehicleTypeID = c.Int(nullable: false),
                        Make = c.String(),
                        Model = c.String(),
                        Year = c.String(),
                        RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.VehicleID)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.ColorID, cascadeDelete: true)
                .Index(t => t.ColorID)
                .Index(t => t.VehicleTypeID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleID = c.Int(nullable: false, identity: true),
                        VehicleID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceDate = c.DateTime(),
                        PaymentRecivedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.SaleID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: true)
                .Index(t => t.VehicleID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAdress = c.String(),
                        Telephone = c.String(),
                        DateOfBirth = c.DateTime(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        VehicleTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.VehicleTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "ColorID", "dbo.Colors");
            DropForeignKey("dbo.Vehicles", "VehicleTypeID", "dbo.VehicleTypes");
            DropForeignKey("dbo.Sales", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.Sales", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Sales", new[] { "CustomerID" });
            DropIndex("dbo.Sales", new[] { "VehicleID" });
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeID" });
            DropIndex("dbo.Vehicles", new[] { "ColorID" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Customers");
            DropTable("dbo.Sales");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Colors");
        }
    }
}
