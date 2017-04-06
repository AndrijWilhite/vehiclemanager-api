namespace VehicleManager.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VehicleManager.API.Data.VehicleManagerDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VehicleManager.API.Data.VehicleManagerDataContext context)
        {
            if (context.Customers.Count() == 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    context.Customers.Add(new Models.Customer
                    {
                        EmailAdress = Faker.InternetFaker.Email(),
                        DateOfBirth = Faker.DateTimeFaker.BirthDay(),
                        FirstName = Faker.NameFaker.FirstName(),
                        LastName = Faker.NameFaker.LastName(),
                        Telephone = Faker.PhoneFaker.Phone()
                    });
                }

                context.SaveChanges();
            }
            string[] colorz = new string[]
            {
                    "Red","White","Black","Blue","Hot Pink"
            };
            if (context.Colors.Count() == 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    context.Colors.Add(new Models.Color
                    {
                        Description = Faker.ArrayFaker.SelectFrom(colorz)
                    });
                }

                context.SaveChanges();

                string[] type = new string[]
                {
                    "SUV","Truck","Coupe","Sedan","Station Wagon","WamBam"
                };
                if (context.VehicleTypes.Count() == 0)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        context.VehicleTypes.Add(new Models.VehicleType
                        {
                            Description = Faker.ArrayFaker.SelectFrom(type)
                        });
                    }

                    context.SaveChanges();
                }
                string[] model = new string[]
                    {
                    "Supper","rzz","F1","whelpatab","WamBam"
                    };
                string[] make = new string[]
                    {
                    "Honda","Toyota","Datsun","Geo"
                    };
                if (context.Vehicles.Count() == 0)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        context.Vehicles.Add(new Models.Vehicle
                        {
                            Make = Faker.ArrayFaker.SelectFrom(make),
                            Model = Faker.ArrayFaker.SelectFrom(model),
                            Year = Faker.StringFaker.Numeric(4),
                            RetailPrice = Faker.NumberFaker.Number(),
                            ColorID = context.Colors.Find(i + 1).ColorID,
                            VehicleTypeID = context.VehicleTypes.Find(i + 1).VehicleTypeID,
                        });
                    }

                    context.SaveChanges();
                }

                if (context.Sales.Count() == 0)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        context.Sales.Add(new Models.Sale
                        {
                            SalePrice = Faker.NumberFaker.Number(),
                            InvoiceDate = Faker.DateTimeFaker.DateTime(),
                            PaymentRecivedDate = Faker.DateTimeFaker.DateTime(),
                            CustomerID = context.Customers.Find(i + 1).CustomerID,
                            VehicleID = context.Vehicles.Find(i + 1).VehicleID
                        });
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
