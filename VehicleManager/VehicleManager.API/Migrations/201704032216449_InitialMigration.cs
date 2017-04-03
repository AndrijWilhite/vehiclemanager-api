namespace VehicleManager.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.VehicleID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Int(nullable: false, identity: true),
                        Make = c.String(),
                        Model = c.String(),
                        Year = c.String(),
                        Color = c.String(),
                        VehicleType = c.String(),
                        RetailPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Sales", "VehicleID", "dbo.Vehicles");
            DropIndex("dbo.Sales", new[] { "CustomerID" });
            DropIndex("dbo.Sales", new[] { "VehicleID" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Sales");
            DropTable("dbo.Customers");
        }
    }
}
