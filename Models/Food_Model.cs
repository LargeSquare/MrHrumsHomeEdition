using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using MrHrumsHomeEdition.Data.DataBaseModels;
using static MrHrumsHomeEdition.OtherClasses.DataBaseConnection;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;

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
            DB.FoodNames.Load();
            DB.FoodWeights.Load();
            DB.Granules.Load();
            DB.Prices.Load();
            DB.Foods.Load();

            FoodNames = DB.FoodNames.Local;
            FoodWeights = DB.FoodWeights.Local;
            Granules = DB.Granules.Local;
            Prices = DB.Prices.Local;
            Foods = DB.Foods.Local;
        }

        public void CreateFoodName(FoodName Name)
        {
            FoodNames.Add(Name);
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 12),
                Date = DateTime.Now,
                Message = string.Format("Создание названия корма: {0}",
                                        Name.Name)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void ChangeFoodName(FoodName OldName, FoodName NewName)
        {
            OldName.Name = NewName.Name;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 13),
                Date = DateTime.Now,
                Message = string.Format("Редактирование названия корма: {0} -> {1}",
                                        OldName.Name, NewName.Name)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void RemoveFoodName(FoodName Name)
        {
            Name.Visible = false;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 14),
                Date = DateTime.Now,
                Message = string.Format("Удаление названия корма: {0}",
                                        Name.Name)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
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
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 15),
                Date = DateTime.Now,
                Message = string.Format("Создание веса корма: {0}",
                                        Weight.Weight)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void ChangeFoodWeight(FoodWeight OldWeight, FoodWeight NewWeight)
        {
            OldWeight.Weight = NewWeight.Weight;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 16),
                Date = DateTime.Now,
                Message = string.Format("Редактирование веса корма: {0} -> {1}",
                                        OldWeight.Weight, NewWeight.Weight)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void RemoveFoodWeight(FoodWeight Weight)
        {
            Weight.Visible = false;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 17),
                Date = DateTime.Now,
                Message = string.Format("Удаление веса корма: {0}",
                                        Weight.Weight)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
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
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 18),
                Date = DateTime.Now,
                Message = string.Format("Создание гранулы корма: {0}",
                                        granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void ChangeGranule(Granule OldGranule, Granule NewGranule) 
        {
            OldGranule.Size = NewGranule.Size;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 19),
                Date = DateTime.Now,
                Message = string.Format("Редактирование гранулы корма: {0} -> {1}",
                                        OldGranule.Size, NewGranule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void RemoveGranule(Granule granule)
        {
            granule.Visible = false;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 20),
                Date = DateTime.Now,
                Message = string.Format("Удаление гранулы корма: {0}",
                                        granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
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


        public void CreateFood(Food food)
        {
            bool CanCreateWarehouse = AppModels.WarehouseModel.CanCreateWarehouseItem(food);
            CreatePrice(food.Price);
            Foods.Add(food);
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 21),
                Date = DateTime.Now,
                Message = string.Format("Создание корма: {0} {1}кг {2}",
                                        food.FoodName.Name,
                                        food.FoodWeight.Weight,
                                        food.Granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
            if (CanCreateWarehouse)
            {
                AppModels.WarehouseModel.CreateWarehouseItemFromFood(food);
            }
        }
        public void ChangeFood(Food OldFood, Food NewFood)
        {
            RemoveFood(OldFood);
            CreatePrice(NewFood.Price);
            CreateFood(NewFood);
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 22),
                Date = DateTime.Now,
                Message = string.Format("Редактирование корма: Закупка - {0} -> {1}, Розница - {2} -> {3}, Питомник - {4} -> {5}",
                                        OldFood.Price.Purchase, NewFood.Price.Purchase,
                                        OldFood.Price.Retail, NewFood.Price.Retail,
                                        OldFood.Price.Kennel, NewFood.Price.Kennel)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void RemoveFood(Food food)
        {
            Warehouse RemovableWarehouse = AppModels.WarehouseModel.GetWarehouseItemByFood(food);
            AppModels.WarehouseModel.RemoveWarehouseItem(RemovableWarehouse);
            food.Visible = false;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 23),
                Date = DateTime.Now,
                Message = string.Format("Удаление корма: {0} {1}кг {2}",
                                        food.FoodName.Name,
                                        food.FoodWeight.Weight,
                                        food.Granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
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
            Warehouse CheckableItem = AppModels.WarehouseModel.GetWarehouseItemByFood(food);
            return AppModels.WarehouseModel.CanRemoveWarehouseItem(CheckableItem);
        }
    }
}
