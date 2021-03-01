using MrHrumsHomeEdition.Data;
using PropertyChanged;
using System;
using System.Collections.Generic;
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
    class Supplies_Model
    {
        public ObservableCollection<PositionInSupply> PositionInSupplies { get; set; }
        public ObservableCollection<Supply> Supplies { get; set; }

        public Supplies_Model()
        {
            DB.PositionInSupplies.Load();
            DB.Supplies.Load();

            PositionInSupplies = DB.PositionInSupplies.Local;
            Supplies = DB.Supplies.Local;
        }

        public void RecalcAmountOfSupply(Supply supply)
        {
            decimal sum = 0;
            foreach (PositionInSupply p in supply.PositionInSupply)
            {
                sum += p.Food.Price.Purchase * p.CountOfBags;
            }
            supply.Amount = sum;
            DB.SaveChanges();
        }


        public void CreateSupply(Supply supply)
        {
            Supplies.Add(supply);
            DB.SaveChanges();
        }
        public void ClearSupply(Supply supply)
        {
            ClearPositionsfromList(supply.PositionInSupply.ToList());
            Supplies.Remove(supply);
            DB.SaveChanges();
        }
        public void UpdateSupply(Supply supply, List<PositionInSupply> positions)
        {
            foreach (PositionInSupply p in positions)
            {
                if (p.CountOfBags != 0)
                {
                    PositionInSupply LocalPosition = supply.PositionInSupply.FirstOrDefault(lp =>
                            lp.Food.FoodName.Name == p.Food.FoodName.Name &&
                            lp.Food.FoodWeight.Weight == p.Food.FoodWeight.Weight &&
                            lp.Food.Granule.Size == p.Food.Granule.Size);
                    if (LocalPosition != null)
                    {
                        if (LocalPosition.CarriedOut || LocalPosition.Paid)
                        {
                            MessageBox.Show(string.Format("Нельзя изменить проведенную или оплаченную позицию ({0} {1}кг {2})!",
                                                            LocalPosition.Food.FoodName.Name,
                                                            LocalPosition.Food.FoodWeight.Weight,
                                                            LocalPosition.Food.Granule.Size), "Ошибка",
                                            MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        LocalPosition.CountOfBags += p.CountOfBags;
                        DB.SaveChanges();
                    }
                    else
                    {
                        CreatePosition(p);
                    }
                }
            }
        }
        public bool CanClearSupply(Supply supply)
        {
            bool result = true;

            foreach (PositionInSupply p in supply.PositionInSupply)
            {
                if(p.Paid || p.CarriedOut)
                {
                    result = false;
                }
            }

            return result;
        }



        public void CreatePosition(PositionInSupply position)
        {
            PositionInSupplies.Add(position);
            DB.SaveChanges();
        }
        public void ChangePosition(PositionInSupply position)
        {
            DB.SaveChanges();
        }
        public void ClearPosition(PositionInSupply position)
        {
            PositionInSupplies.Remove(position);
            DB.SaveChanges();
        }
        public void CreatePositionsfromList(List<PositionInSupply> positions)
        {
            foreach (PositionInSupply p in positions)
            {
                if(p.CountOfBags != 0)
                {
                    CreatePosition(p);
                }
            }
        }
        public void ClearPositionsfromList(List<PositionInSupply> positions)
        {
            foreach (PositionInSupply p in positions)
            {
                ClearPosition(p);
            }
        }

        public bool CanCreatePositionInSupply(Supply supply)
        {
            bool Result = !supply.CarriedOut;
            return Result;
        }
        public bool CanChangePosition(PositionInSupply position)
        {
            bool Result = true;
            if(position.CarriedOut || position.Paid)
            {
                Result = false;
            }
            return Result;
        }
        public bool CanClearPositionInSupply(PositionInSupply position)
        {
            bool Result = true;
            if(position.CarriedOut || position.Paid)
            {
                Result = false;
            }
            return Result;
        }



        public void SetCarriedOutForSupply(Supply supply)
        {
            foreach(PositionInSupply p in supply.PositionInSupply)
            {
                if (!p.CarriedOut)
                {
                    p.CarriedOut = supply.CarriedOut;
                    SetCarriedOutForPosition(p);
                }
            }
            DB.SaveChanges();
        }
        public void SetPaidForSupply(Supply supply)
        {
            foreach (PositionInSupply p in supply.PositionInSupply)
            {
                p.Paid = supply.Paid;
                SetPaidForPosition(p);
            }
            DB.SaveChanges();
        }
        public bool CanSetCarriedOutForSupply(Supply supply)
        {
            bool Result = false;
            if (supply.CarriedOut)
            {
                Result = true;
            }

            return Result;
        }


        public void SetCarriedOutForPosition(PositionInSupply position)
        {
            Warehouse LocalWarehouse = AppModels.WarehouseModel.GetWarehouseItemByFood(position.Food);
            switch (position.CarriedOut)
            {
                case true:

                    bool CanSetCarriedOutForSupply = true;
                    foreach (PositionInSupply p in position.Supply.PositionInSupply)
                    {
                        if (p.CarriedOut == false)
                        {
                            CanSetCarriedOutForSupply = false;
                        }
                    }
                    if (CanSetCarriedOutForSupply)
                    {
                        position.Supply.CarriedOut = CanSetCarriedOutForSupply;
                    }

                    AppModels.WarehouseModel.AddBagsToItem(LocalWarehouse, position.CountOfBags);
                    break;

                case false:
                    if (AppModels.WarehouseModel.CanRemoveBagsFromItem(LocalWarehouse, position.CountOfBags))
                    {
                        AppModels.WarehouseModel.RemoveBagsFromItem(LocalWarehouse, position.CountOfBags);
                    }
                    else
                    {
                        MessageBox.Show("На складе недостаточно мешков!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    break;
            }
            DB.SaveChanges();
        }
        public bool CanSetCarriedOutForPosition(PositionInSupply position)
        {
            bool Result = false;

            Warehouse SearchableWarehouse = AppModels.WarehouseModel.GetWarehouseItemByFood(position.Food);
            if(SearchableWarehouse != null && position.Supply.CarriedOut == false)
            {
                Result = true;
            }

            return Result;
            
        }
        public void SetPaidForPosition(PositionInSupply position)
        {
            int index = 0;
            switch (position.Paid)
            {
                case true:
                    bool CanSetPaidForSupply = true;
                    foreach(PositionInSupply p in position.Supply.PositionInSupply)
                    {
                        if (p.Paid == false)
                        {
                            CanSetPaidForSupply = false;
                        }
                    }
                    if (CanSetPaidForSupply)
                    {
                        position.Supply.Paid = CanSetPaidForSupply;
                    }
                    index = 1;
                    break;

                case false:
                    position.Supply.Paid = false;
                    index = 8;
                    break;
            }

            Budget NewItemForAddCash = new Budget()
            {
                Amount = position.CountOfBags * position.Food.Price.Purchase,
                BudgetState = AppModels.BudgetModel.BudgetStates.FirstOrDefault(b => b.Id == index),
                Date = DateTime.Now
            };

            AppModels.BudgetModel.AddBudgetItem(NewItemForAddCash);
            DB.SaveChanges();
        }
    }
}
