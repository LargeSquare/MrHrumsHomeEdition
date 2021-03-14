using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.Data.DataBaseModels;
using static MrHrumsHomeEdition.OtherClasses.DataBaseConnection;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Models
{
    class Orders_Model : BasePropertyChanged
    {
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<PositionInOrder> PositionInOrders { get; set; }
        public ObservableCollection<TypeOfSale> TypeOfSales { get; set; }
        public ObservableCollection<TypeOfDelivery> TypeOfDeliveries { get; set; }


        public Orders_Model()
        {
            DB.Orders.Load();
            DB.PositionInOrders.Load();
            DB.TypeOfSales.Load();
            DB.TypeOfDeliveries.Load();

            Orders = DB.Orders.Local;
            PositionInOrders = DB.PositionInOrders.Local;
            TypeOfSales = DB.TypeOfSales.Local;
            TypeOfDeliveries = DB.TypeOfDeliveries.Local;
        }

        



        public void CreateOrder(Order order)
        {
            Orders.Add(order);
            DB.SaveChanges();
        }
        public void ClearOrder(Order order)
        {
            ClearPositionsfromList(order.PositionInOrder.ToList());
            Orders.Remove(order);
            DB.SaveChanges();
        }
        public void UpdateOrder()
        {
            // todo: make this method
        }
        public bool CanClearOrder(Order order)
        {
            bool result = true;

            foreach (PositionInOrder p in order.PositionInOrder)
            {
                if (p.Paid || p.CarriedOut || order.DeliveryPaid)
                {
                    result = false;
                }
            }

            return result;
        }



        public void CreatePosition(PositionInOrder position)
        {
            PositionInOrders.Add(position);
            DB.SaveChanges();
        }
        public void ChangePosition(PositionInOrder position)
        {
            DB.SaveChanges();
        }
        public void ClearPosition(PositionInOrder position)
        {
            PositionInOrders.Remove(position);
            DB.SaveChanges();
        }
        public void CreatePositionsfromList(List<PositionInOrder> positions)
        {
            foreach (PositionInOrder p in positions)
            {
                if (p.CountOfBags != 0 || p.CountOfKG != 0)
                {
                    CreatePosition(p);
                }
            }
        }
        public void ClearPositionsfromList(List<PositionInOrder> positions)
        {
            foreach (PositionInOrder p in positions)
            {
                ClearPosition(p);
            }
        }





        public bool CanCreatePositionInOrder(Order order)
        {
            bool Result = !order.CarriedOut;
            return Result;
        }
        public bool CanChangePosition(PositionInOrder position)
        {
            bool Result = true;
            if (position.CarriedOut || position.Paid)
            {
                Result = false;
            }
            return Result;
        }
        public bool CanClearPositionInOrder(PositionInOrder position)
        {
            bool Result = true;
            if (position.CarriedOut || position.Paid)
            {
                Result = false;
            }
            return Result;
        }



        // todo: make all this methods
        public void SetCarriedOutForOrder(Order order)
        {

        }
        public void SetPaidForOrder(Order order)
        {

        }
        public void CanSetCarriedOutForOrder(Order order)
        {

        }




        public void SetCarriedOutForPosition(PositionInOrder position)
        {

        }
        public void CanSetCarriedOutForPosition(PositionInOrder position)
        {

        }
        public void SetPaidForPosition(PositionInOrder position)
        {

        }
        public void some() // body once told me
        {

        }
    }
}
