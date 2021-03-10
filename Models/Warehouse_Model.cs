using MrHrumsHomeEdition.Data;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using MrHrumsHomeEdition.Data.DataBaseModels;
using static MrHrumsHomeEdition.OtherClasses.DataBaseConnection;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Models
{
    class Warehouse_Model : BasePropertyChanged
    {
        public ObservableCollection<Warehouse> WarehouseItems { get; set; }
        public ObservableCollection<Bag> WarehouseBagsItems { get; set; }
        public ObservableCollection<KG> WarehouseKgItems { get; set; }

        public Warehouse_Model()
        {
            DB.Warehouses.Load();
            DB.Bags.Load();
            DB.KGs.Load();

            WarehouseItems = DB.Warehouses.Local;
            WarehouseBagsItems = DB.Bags.Local;
            WarehouseKgItems = DB.KGs.Local;
        }


        /*public void UpdateFoodInWarehouse(Food FoodItem)
        {
            Warehouse WarehouseItem = GetWarehouseItemByParams(
                FoodItem.FoodName.Name,
                FoodItem.FoodWeight.Weight,
                FoodItem.Granule);

            WarehouseKG WarehouseKGItem = GetWarehouseKGItemByParams(
                FoodItem.FoodName.Name,
                FoodItem.Granule);

            WarehouseItem.Food = FoodItem;
            WarehouseKGItem.Food = FoodItem;
            DB.SaveChanges();
        }*/


        //----------------- Working with Warehouse objects -----------
        public Warehouse GetWarehouseItemByFood(Food FoodItem)
        {
            Warehouse Item = WarehouseItems.FirstOrDefault(w =>
               w.Food.FoodName.Name == FoodItem.FoodName.Name &&
               w.Food.FoodWeight.Weight == FoodItem.FoodWeight.Weight &&
               w.Food.Granule.Size == FoodItem.Granule.Size);
            
            return Item;
        }

        public Warehouse CheckWarehouseForFood(Food FoodItem) {
            Warehouse Item = WarehouseItems.FirstOrDefault(w =>
               w.Food.FoodName.Name == FoodItem.FoodName.Name &&
               w.Food.Granule.Size == FoodItem.Granule.Size);
            
            return Item;
        }

        public void CreateWarehouseItemFromFood(Food FoodItem)
        {
            Warehouse Item = new Warehouse();
            Bag Bag = new Bag();
            KG Kg = new KG();

            WarehouseBagsItems.Add(Bag);

            Warehouse CheckableItem = CheckWarehouseForFood(FoodItem);
            if(CheckableItem != null)
            {
                Kg = CheckableItem.KG;
            }
            else
            {
                WarehouseKgItems.Add(Kg);
            }

            Item.Food = FoodItem;
            Item.Bags = Bag;
            Item.KG = Kg;

            WarehouseItems.Add(Item);
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 24),
                Date = DateTime.Now,
                Message = string.Format("Создание корма на складе: {0} {1}кг {2}",
                                        FoodItem.FoodName.Name,
                                        FoodItem.FoodWeight.Weight,
                                        FoodItem.Granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void RemoveWarehouseItem(Warehouse WarehouseItem)
        {
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 25),
                Date = DateTime.Now,
                Message = string.Format("Удаление корма со склада: {0} {1}кг {2}",
                                        WarehouseItem.Food.FoodName.Name,
                                        WarehouseItem.Food.FoodWeight.Weight,
                                        WarehouseItem.Food.Granule.Size)
            };
            WarehouseItems.Remove(WarehouseItem);
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public bool CanCreateWarehouseItem(Food FoodItem)
        {
            Warehouse CheckableItem = GetWarehouseItemByFood(FoodItem);

            bool Result = CheckableItem == null ? true : false;
            return Result;
        }
        public bool CanRemoveWarehouseItem(Warehouse WarehouseItem)
        {
            bool Result = false;
            if (WarehouseItem.Bags.Count == 0 && WarehouseItem.KG.Count == 0)
            {
                Result = true;
            }
            return Result;
        }




        public void AddBagsToItem(Warehouse WarehouseItem, int Count)
        {
            WarehouseItem.Bags.Count += Count;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 26),
                Date = DateTime.Now,
                Message = string.Format("Добавление {0} мешков корма {1} {2}кг {3} на склад",
                                        Count,
                                        WarehouseItem.Food.FoodName.Name,
                                        WarehouseItem.Food.FoodWeight.Weight,
                                        WarehouseItem.Food.Granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void RemoveBagsFromItem(Warehouse WarehouseItem, int Count)
        {
            WarehouseItem.Bags.Count -= Count;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 27),
                Date = DateTime.Now,
                Message = string.Format("Удаление {0} мешков корма {1} {2}кг {3} со склада",
                                        Count,
                                        WarehouseItem.Food.FoodName.Name,
                                        WarehouseItem.Food.FoodWeight.Weight,
                                        WarehouseItem.Food.Granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public bool CanRemoveBagsFromItem(Warehouse WarehouseItem, int Count)
        {
            bool Result = true;
            if(WarehouseItem.Bags.Count - Count < 0)
            {
                Result = false;
            }
            return Result;
        }

        public void AddKGToItem(Warehouse WarehouseItem, int Count)
        {
            WarehouseItem.KG.Count += Count;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 28),
                Date = DateTime.Now,
                Message = string.Format("Добавление {0} кг корма {1} {2} на склад",
                                        Count,
                                        WarehouseItem.Food.FoodName.Name,
                                        WarehouseItem.Food.Granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public void RemoveKGFromItem(Warehouse WarehouseItem, int Count)
        {
            WarehouseItem.KG.Count -= Count;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 29),
                Date = DateTime.Now,
                Message = string.Format("Удаление {0} кг корма {1} {2} со склада",
                                        Count,
                                        WarehouseItem.Food.FoodName.Name,
                                        WarehouseItem.Food.Granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
        public bool CanRemoveKGFromItem(Warehouse WarehouseItem, int Count)
        {
            bool Result = true;
            if (WarehouseItem.KG.Count - Count < 0)
            {
                Result = false;
            }
            return Result;
        }






        //----------------- Working with Pack and Unpack bags -----------
        public void PackingItem(Warehouse WarehouseItem, int CountOfBags)
        {
            // CountOfBags - count of bags that i want to get

            int CountOfKG = (int)(CountOfBags * WarehouseItem.Food.FoodWeight.Weight);
            RemoveKGFromItem(WarehouseItem, CountOfKG);
            AddBagsToItem(WarehouseItem, CountOfBags);

            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 30),
                Date = DateTime.Now,
                Message = string.Format("Запаковка {0} мешков корма {1} {2}кг {3}",
                                        CountOfBags,
                                        WarehouseItem.Food.FoodName.Name,
                                        WarehouseItem.Food.FoodWeight.Weight,
                                        WarehouseItem.Food.Granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
        }
        public void UnpackingItem(Warehouse WarehouseItem, int CountOfBags)
        {
            // CountOfBags - count of bags that i want to unpack

            int CountOfKG = (int)(CountOfBags * WarehouseItem.Food.FoodWeight.Weight);
            RemoveBagsFromItem(WarehouseItem, CountOfBags);
            AddKGToItem(WarehouseItem, CountOfKG);

            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 30),
                Date = DateTime.Now,
                Message = string.Format("Распаковка {0} мешков корма {1} {2}кг {3}",
                                        CountOfBags,
                                        WarehouseItem.Food.FoodName.Name,
                                        WarehouseItem.Food.FoodWeight.Weight,
                                        WarehouseItem.Food.Granule.Size)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
        }
        public bool CanPackingItem(Warehouse WarehouseItem, int CountOfBags)
        {
            bool Result = false;


            int CountOfKG = (int)(CountOfBags * WarehouseItem.Food.FoodWeight.Weight);
            if (CanRemoveKGFromItem(WarehouseItem, CountOfKG))
            {
                Result = true;
            }
            return Result;
        }
        public bool CanUnpackingItem(Warehouse WarehouseItem, int CountOfBags)
        {
            bool Result = false;
            if(CanRemoveBagsFromItem(WarehouseItem, CountOfBags))
            {
                Result = true;
            }
            return Result;
        }
    }
}
