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


        public void CreateSupply(Supply supply)
        {
            Supplies.Add(supply);
            DB.SaveChanges();
        }
        public void ClearSupply(Supply supply) { }



        public void CreatePosition(PositionInSupply position)
        {
            PositionInSupplies.Add(position);
            DB.SaveChanges();
        }
        public void ChangePosition()
        {
            
        }
        public void ClearPosition() { }
        public void CreatePositionsfromList(List<PositionInSupply> positions)
        {
            foreach (PositionInSupply p in positions)
            {
                CreatePosition(p);
            }
        }
        public void ClearPositionsfromList() { }



        public void SetCarriedOutForSupply() { }
        public void SetCarriedOutForPosition() { }
        public void CanSetCarriedOutForSupply() { }
        public void CanSetCarriedOutForPosition(PositionInSupply position)
        {

        }
        public void SetPaidForSupply() { }
        public void SetPaidForPosition() { }
    }
}
