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
    class Supplies_Model
    {
        public ObservableCollection<PositionInSupply> PositionInSupplies { get; set; }
        public ObservableCollection<Supply> Supplies { get; set; }

        public Supplies_Model()
        {
            DB.PositionInSupply.Load();
            DB.Supply.Load();

            PositionInSupplies = DB.PositionInSupply.Local;
            Supplies = DB.Supply.Local;
        }


        public void CreateSupply() { }
        public void ChangeSupply() { }
        public void ReturnSupply() { }
        public void ClearSupply() { }



        public void CreatePosition() { }
        public void ChangePosition() { }
        public void ReturnPosition() { }
        public void ClearPosition() { }
        public void CreatePositionsfromList() { }
        public void ChangePositionsfromList() { }
        public void ReturnPositionsfromList() { }
        public void ClearePositionsfromList() { }



        public void SetCarriedOutForSupply() { }
        public void SetCarriedOutForPosition() { }
        public void SetPaidForSupply() { }
        public void SetPaidForPosition() { }
    }
}
