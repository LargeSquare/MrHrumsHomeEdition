using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using static MrHrumsHomeEdition.OtherClasses.DataBaseConnection;

namespace MrHrumsHomeEdition.Models
{
    [AddINotifyPropertyChangedInterface]
    class Food_Model
    {
        public ObservableCollection<FoodName> FoodNames { get; set; }
        public ObservableCollection<FoodWeight> FoodWeights { get; set; }
        public ObservableCollection<Granule> Granules { get; set; }
        public ObservableCollection<Price> Prices { get; set; }
        public ObservableCollection<Food> Foods { get; set; }

        public Food_Model()
        {
            DB.FoodName.Load();
            DB.FoodWeight.Load();
            DB.Granule.Load();
            DB.Price.Load();
            DB.Food.Load();

            FoodNames = DB.FoodName.Local;
            FoodWeights = DB.FoodWeight.Local;
            Granules = DB.Granule.Local;
            Prices = DB.Price.Local;
            Foods = DB.Food.Local;
        }

        public void CreateFoodName(FoodName Name)
        {
            FoodNames.Add(Name);
            DB.SaveChanges();
        }
        public void ChangeFoodName(FoodName OldName, FoodName NewName)
        {
            OldName.Name = NewName.Name;
            DB.SaveChanges();
        }
        public void RemoveFoodName(FoodName Name)
        {
            Name.Visible = false;
            DB.SaveChanges();
        }
        public bool CanCreateFoodName(FoodName Name) 
        {
            FoodName CheckableItem = FoodNames.FirstOrDefault(n =>
                                               n.Name == Name.Name &&
                                               n.Visible == true);
            bool Result = CheckableItem == null ? true : false;
            return Result;
        }
        public bool CanRemoveFoodName(FoodName Name)
        {
            Food CheckableItem = Foods.FirstOrDefault(n =>
                                               n.FoodName.Name == Name.Name &&
                                               n.Visible == true);
            bool Result = CheckableItem == null ? true : false;
            return Result;
        }


        public void CreateFoodWeight(FoodWeight Weight)
        {
            FoodWeights.Add(Weight);
            DB.SaveChanges();
        }
        public void ChangeFoodWeight(FoodWeight OldWeight, FoodWeight NewWeight)
        {
            OldWeight.Weight = NewWeight.Weight;
            DB.SaveChanges();
        }
        public void RemoveFoodWeight(FoodWeight Weight)
        {
            FoodWeights.Remove(Weight);
            DB.SaveChanges();
        }
        public bool CanCreateFoodWeight(FoodWeight Weight)
        {
            FoodWeight CheckableItem = FoodWeights.FirstOrDefault(w =>
                                               w.Weight == Weight.Weight &&
                                               w.Visible == true);
            bool Result = CheckableItem == null ? true : false;
            return Result;
        }
        public bool CanRemoveFoodWeight(FoodWeight Weight)
        {
            Food CheckableItem = Foods.FirstOrDefault(w =>
                                               w.FoodWeight.Weight == Weight.Weight &&
                                               w.Visible == true);
            bool Result = CheckableItem == null ? true : false;
            return Result;
        }


        public void CreateGranule(Granule granule) 
        {
            Granules.Add(granule);
            DB.SaveChanges();
        }
        public void ChangeGranule(Granule OldGranule, Granule NewGranule) 
        {
            OldGranule.Size = NewGranule.Size;
            DB.SaveChanges();
        }
        public void RemoveGranule(Granule granule)
        {
            Granules.Remove(granule);
            DB.SaveChanges();
        }
        public bool CanCreateGranule(Granule granule) 
        {
            Granule CheckableItem = Granules.FirstOrDefault(g =>
                                               g.Size == granule.Size &&
                                               g.Visible == true);
            bool Result = CheckableItem == null ? true : false;
            return Result;
        }
        public bool CanRemoveGranule(Granule granule) 
        {
            Food CheckableItem = Foods.FirstOrDefault(g =>
                                               g.Granule.Size == granule.Size &&
                                               g.Visible == true);
            bool Result = CheckableItem == null ? true : false;
            return Result;
        }

        public void CreatePrice(Price price)
        {
            price.Date = DateTime.Now;
            Prices.Add(price);
            DB.SaveChanges();
        }

        // todo: remove this method if CreatePrice will work fine
        /*public Price CreatePriceAndReturn(Price price)
        {
            Prices.Add(price);
            DB.SaveChanges();
            return Prices.OrderByDescending(p => p.Id).FirstOrDefault();
        }*/


        public void CreateFood(Food food)
        {
            bool CanCreateWarehouse = OtherClasses.Models.WarehouseModel.CanCreateWarehouseItem(food);
            CreatePrice(food.Price);
            Foods.Add(food);
            DB.SaveChanges();
            if (CanCreateWarehouse)
            {
                OtherClasses.Models.WarehouseModel.CreateWarehouseItemFromFood(food);
            }
            else
            {
                /*MessageBox.Show(
                    "Невозможно создать корм!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;*/
            }
        }
        public void ChangeFood(Food OldFood, Food NewFood)
        {
            RemoveFood(OldFood);
            CreatePrice(NewFood.Price);
            CreateFood(NewFood);
            DB.SaveChanges();
        }
        public void RemoveFood(Food food)
        {
            Warehouse RemovableWarehouse = OtherClasses.Models.WarehouseModel.GetWarehouseItemByFood(food);
            OtherClasses.Models.WarehouseModel.RemoveWarehouseItem(RemovableWarehouse);
            food.Visible = false;
            DB.SaveChanges();
        }
        public bool CanCreateFood(Food food)
        {
            Food CheckableItem = Foods.FirstOrDefault(f =>
                                       f.FoodName.Name == food.FoodName.Name &&
                                       f.FoodWeight.Weight == food.FoodWeight.Weight &&
                                       f.Granule.Size == food.Granule.Size &&
                                       f.Visible == true);
            bool Result = CheckableItem == null ? true : false;
            return Result;
        }
        public bool CanRemoveFood(Food food)
        {
            Warehouse CheckableItem = OtherClasses.Models.WarehouseModel.GetWarehouseItemByFood(food);
            return OtherClasses.Models.WarehouseModel.CanRemoveWarehouseItem(CheckableItem);
        }
    }
}
