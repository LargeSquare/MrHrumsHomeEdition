namespace MrHrumsHomeEdition.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitSignature : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bags",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Count = c.Int(nullable: false, defaultValue: 0),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Warehouses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FoodID = c.Int(nullable: false),
                    BagsID = c.Int(nullable: false),
                    KgID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bags", t => t.BagsID, cascadeDelete: true)
                .ForeignKey("dbo.Foods", t => t.FoodID, cascadeDelete: true)
                .ForeignKey("dbo.KGs", t => t.KgID, cascadeDelete: true)
                .Index(t => t.FoodID)
                .Index(t => t.BagsID)
                .Index(t => t.KgID);

            CreateTable(
                "dbo.Foods",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FoodNameID = c.Int(nullable: false),
                    FoodWeightID = c.Int(nullable: false),
                    GranuleID = c.Int(nullable: false),
                    PriceID = c.Int(nullable: false),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodNames", t => t.FoodNameID, cascadeDelete: true)
                .ForeignKey("dbo.FoodWeights", t => t.FoodWeightID, cascadeDelete: true)
                .ForeignKey("dbo.Granules", t => t.GranuleID, cascadeDelete: true)
                .ForeignKey("dbo.Prices", t => t.PriceID, cascadeDelete: true)
                .Index(t => t.FoodNameID)
                .Index(t => t.FoodWeightID)
                .Index(t => t.GranuleID)
                .Index(t => t.PriceID);

            CreateTable(
                "dbo.FoodNames",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.FoodWeights",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Weight = c.Double(nullable: false),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Granules",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Size = c.String(nullable: false, maxLength: 50),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PositionInOrders",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    OrderID = c.Int(nullable: false),
                    FoodID = c.Int(nullable: false),
                    TypeOfSaleID = c.Int(nullable: false),
                    CountOfBags = c.Int(nullable: false, defaultValue: 0),
                    CountOfKG = c.Int(nullable: false, defaultValue: 0),
                    IndividualPrice = c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 0),
                    Discount = c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 0),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CarriedOut = c.Boolean(nullable: false, defaultValue: false),
                    Paid = c.Boolean(nullable: false, defaultValue: false),
                    Returned = c.Boolean(nullable: false, defaultValue: false),
                    ReturnedKG = c.Int(nullable: false, defaultValue: 0),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.FoodID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfSales", t => t.TypeOfSaleID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.FoodID)
                .Index(t => t.TypeOfSaleID);

            CreateTable(
                "dbo.Orders",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ClientID = c.Int(nullable: false),
                    TypeOfDeliveryID = c.Int(nullable: false),
                    CostOfDelivery = c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 0),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CarriedOut = c.Boolean(nullable: false, defaultValue: false),
                    Paid = c.Boolean(nullable: false, defaultValue: false),
                    DeliveryPaid = c.Boolean(nullable: false, defaultValue: false),
                    Note = c.String(maxLength: 4000),
                    Date = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfDeliveries", t => t.TypeOfDeliveryID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.TypeOfDeliveryID);

            CreateTable(
                "dbo.Clients",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Address = c.String(nullable: false, maxLength: 4000),
                    Number = c.String(nullable: false, maxLength: 50),
                    Note = c.String(maxLength: 4000),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TypeOfDeliveries",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Type = c.String(nullable: false, maxLength: 50),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                    IsSystem = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TypeOfSales",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Type = c.String(nullable: false, maxLength: 50),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                    IsSystem = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PositionInSupplies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SupplyID = c.Int(nullable: false),
                    FoodID = c.Int(nullable: false),
                    CountOfBags = c.Int(nullable: false),
                    CarriedOut = c.Boolean(nullable: false, defaultValue: false),
                    Paid = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.FoodID, cascadeDelete: true)
                .ForeignKey("dbo.Supplies", t => t.SupplyID, cascadeDelete: true)
                .Index(t => t.SupplyID)
                .Index(t => t.FoodID);

            CreateTable(
                "dbo.Supplies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CarriedOut = c.Boolean(nullable: false, defaultValue: false),
                    Paid = c.Boolean(nullable: false, defaultValue: false),
                    Date = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Prices",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Retail = c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 0),
                    Kennel = c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 0),
                    Purchase = c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 0),
                    ForSelf = c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 0),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                    Date = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.KGs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Count = c.Int(nullable: false, defaultValue: 0),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Budgets",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    BudgetStateID = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                    Note = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BudgetStates", t => t.BudgetStateID, cascadeDelete: true)
                .Index(t => t.BudgetStateID);

            CreateTable(
                "dbo.BudgetStates",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    TypeOfBudgetActionID = c.Int(nullable: false),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                    IsSystem = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfBudgetActions", t => t.TypeOfBudgetActionID, cascadeDelete: true)
                .Index(t => t.TypeOfBudgetActionID);

            CreateTable(
                "dbo.TypeOfBudgetActions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    IsSystem = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.Events",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeOfEventID = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                    Message = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfEvents", t => t.TypeOfEventID, cascadeDelete: true)
                .Index(t => t.TypeOfEventID);

            CreateTable(
                "dbo.TypeOfEvents",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Type = c.String(nullable: false, maxLength: 50),
                    IsSystem = c.Boolean(nullable: false, defaultValue: false),
                    Visible = c.Boolean(nullable: false, defaultValue: true),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Events", "TypeOfEventID", "dbo.TypeOfEvents");
            DropForeignKey("dbo.BudgetStates", "TypeOfBudgetActionID", "dbo.TypeOfBudgetActions");
            DropForeignKey("dbo.Budgets", "BudgetStateID", "dbo.BudgetStates");
            DropForeignKey("dbo.Warehouses", "KgID", "dbo.KGs");
            DropForeignKey("dbo.Warehouses", "FoodID", "dbo.Foods");
            DropForeignKey("dbo.Foods", "PriceID", "dbo.Prices");
            DropForeignKey("dbo.PositionInSupplies", "SupplyID", "dbo.Supplies");
            DropForeignKey("dbo.PositionInSupplies", "FoodID", "dbo.Foods");
            DropForeignKey("dbo.PositionInOrders", "TypeOfSaleID", "dbo.TypeOfSales");
            DropForeignKey("dbo.Orders", "TypeOfDeliveryID", "dbo.TypeOfDeliveries");
            DropForeignKey("dbo.PositionInOrders", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.PositionInOrders", "FoodID", "dbo.Foods");
            DropForeignKey("dbo.Foods", "GranuleID", "dbo.Granules");
            DropForeignKey("dbo.Foods", "FoodWeightID", "dbo.FoodWeights");
            DropForeignKey("dbo.Foods", "FoodNameID", "dbo.FoodNames");
            DropForeignKey("dbo.Warehouses", "BagsID", "dbo.Bags");
            DropIndex("dbo.Events", new[] { "TypeOfEventID" });
            DropIndex("dbo.TypeOfBudgetActions", new[] { "Name" });
            DropIndex("dbo.BudgetStates", new[] { "TypeOfBudgetActionID" });
            DropIndex("dbo.Budgets", new[] { "BudgetStateID" });
            DropIndex("dbo.PositionInSupplies", new[] { "FoodID" });
            DropIndex("dbo.PositionInSupplies", new[] { "SupplyID" });
            DropIndex("dbo.Orders", new[] { "TypeOfDeliveryID" });
            DropIndex("dbo.Orders", new[] { "ClientID" });
            DropIndex("dbo.PositionInOrders", new[] { "TypeOfSaleID" });
            DropIndex("dbo.PositionInOrders", new[] { "FoodID" });
            DropIndex("dbo.PositionInOrders", new[] { "OrderID" });
            DropIndex("dbo.Foods", new[] { "PriceID" });
            DropIndex("dbo.Foods", new[] { "GranuleID" });
            DropIndex("dbo.Foods", new[] { "FoodWeightID" });
            DropIndex("dbo.Foods", new[] { "FoodNameID" });
            DropIndex("dbo.Warehouses", new[] { "KgID" });
            DropIndex("dbo.Warehouses", new[] { "BagsID" });
            DropIndex("dbo.Warehouses", new[] { "FoodID" });
            DropTable("dbo.TypeOfEvents");
            DropTable("dbo.Events");
            DropTable("dbo.TypeOfBudgetActions");
            DropTable("dbo.BudgetStates");
            DropTable("dbo.Budgets");
            DropTable("dbo.KGs");
            DropTable("dbo.Prices");
            DropTable("dbo.Supplies");
            DropTable("dbo.PositionInSupplies");
            DropTable("dbo.TypeOfSales");
            DropTable("dbo.TypeOfDeliveries");
            DropTable("dbo.Clients");
            DropTable("dbo.Orders");
            DropTable("dbo.PositionInOrders");
            DropTable("dbo.Granules");
            DropTable("dbo.FoodWeights");
            DropTable("dbo.FoodNames");
            DropTable("dbo.Foods");
            DropTable("dbo.Warehouses");
            DropTable("dbo.Bags");
        }
    }
}
