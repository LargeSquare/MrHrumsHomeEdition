using MrHrumsHomeEdition.Data.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrHrumsHomeEdition.Data
{
    public class MainDataBaseEntity : DbContext
    {
        public DbSet<Bag> Bags { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetState> BudgetStates { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodName> FoodNames { get; set; }
        public DbSet<FoodWeight> FoodWeights { get; set; }
        public DbSet<Granule> Granules { get; set; }
        public DbSet<KG> KGs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PositionInOrder> PositionInOrders { get; set; }
        public DbSet<PositionInSupply> PositionInSupplies { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<TypeOfBudgetAction> TypeOfBudgetActions { get; set; }
        public DbSet<TypeOfDelivery> TypeOfDeliveries { get; set; }
        public DbSet<TypeOfEvent> TypeOfEvents { get; set; }
        public DbSet<TypeOfSale> TypeOfSales { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}
