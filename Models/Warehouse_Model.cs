using MrHrumsHomeEdition.Data;
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
    class Warehouse_Model
    {
        public ObservableCollection<Warehouse> WarehouseItems { get; set; }
        public ObservableCollection<Bags> WarehouseBagsItems { get; set; }
        public ObservableCollection<KG> WarehouseKgItems { get; set; }

        public Warehouse_Model()
        {
            DB.Warehouse.Load();
            DB.Bags.Load();
            DB.KG.Load();

            WarehouseItems = new ObservableCollection<Warehouse>(
                DB.Warehouse.Local
                            .OrderBy(w => w.Food.FoodName.Name)
                            .ThenBy(w => w.Food.FoodWeight.Weight)
                            .ThenBy(w => w.Food.Granule.Size)
                            .ToList()
                );

            WarehouseBagsItems = DB.Bags.Local;
            WarehouseKgItems = DB.KG.Local;
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
            if (Item == null)
            {
                MessageBox.Show("Корм (Мешки) на складе не найден!");
            }
            return Item;
        }

        public Warehouse CheckWarehouseForFood(Food FoodItem) {
            Warehouse Item = WarehouseItems.FirstOrDefault(w =>
               w.Food.FoodName.Name == FoodItem.FoodName.Name &&
               w.Food.Granule.Size == FoodItem.Granule.Size);
            if (Item == null)
            {
                MessageBox.Show("Корм (Мешки) на складе не найден!");
            }
            return Item;
        }

        public void CreateWarehouseItemFromFood(Food FoodItem)
        {
            // todo: Do test for this method
            Warehouse Item = new Warehouse();
            Bags Bag = new Bags();
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
            DB.SaveChanges();
        }
        public void RemoveWarehouseItem(Warehouse WarehouseItem)
        {
            WarehouseItems.Remove(WarehouseItem);
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
            DB.SaveChanges();
        }
        public void RemoveBagsFromItem(Warehouse WarehouseItem, int Count)
        {
            WarehouseItem.Bags.Count -= Count;
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
            DB.SaveChanges();
        }
        public void RemoveKGFromItem(Warehouse WarehouseItem, int Count)
        {
            WarehouseItem.KG.Count -= Count;
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
        }
        public void UnpackingItem(Warehouse WarehouseItem, int CountOfBags)
        {
            // CountOfBags - count of bags that i want to unpack

            int CountOfKG = (int)(CountOfBags * WarehouseItem.Food.FoodWeight.Weight);
            RemoveBagsFromItem(WarehouseItem, CountOfBags);
            AddKGToItem(WarehouseItem, CountOfKG);
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
