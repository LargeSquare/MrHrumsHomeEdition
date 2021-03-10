using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using MrHrumsHomeEdition.Views.Client;
using MrHrumsHomeEdition.Views.Event;
using System.Collections.ObjectModel;
using System.Windows;
using MrHrumsHomeEdition.Data.DataBaseModels;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;
using MrHrumsHomeEdition.Views.Order;
using System;
using System.Linq;
using System.Windows.Data;
using System.ComponentModel;

namespace MrHrumsHomeEdition.ViewModels
{
    class Orders_ViewModel : BasePropertyChanged
    {
        public ObservableCollection<Order> Orders => AppModels.OrdersModel.Orders;
        public ObservableCollection<PositionInOrder> PositionInOrders { get; set; }
        public ObservableCollection<TypeOfSale> TypeOfSales => AppModels.OrdersModel.TypeOfSales;
        public ObservableCollection<TypeOfDelivery> TypeOfDeliveries => AppModels.OrdersModel.TypeOfDeliveries;
        public ObservableCollection<Client> Clients => AppModels.ClientsModel.Clients;


        public Order NewDataForOrder { get; set; }
        public ObservableCollection<PositionInOrder> NewDataForPositions { get; set; }


        public Order SelectedOrder { get; set; }
        public PositionInOrder SelectedPositionInOrder { get; set; }
        public PositionInOrder SelectedNewPosition { get; set; }
        public TypeOfSale SelectedTypeOfSale { get; set; }
        public Client SelectedClient { get; set; }
        public int SelectedDiscount { get; set; } = 0;


        //commands
        public DelegateCommand OpenWindowOfCreateOrder { get; set; }
        public DelegateCommand OpenWindowOfChangeOrder { get; set; }
        public DelegateCommand OpenWindowOfCreatePositions { get; set; }
        public DelegateCommand OpenWindowOfReturnPosition { get; set; }
        public DelegateCommand CreateOrder { get; set; }
        public DelegateCommand ClearOrder { get; set; }
        public DelegateCommand CreatePositions { get; set; }
        public DelegateCommand ChangePosition { get; set; }
        public DelegateCommand ReturnPosition { get; set; }
        public DelegateCommand ClearPosition { get; set; }


        public DelegateCommand OrderPaidChanged { get; set; }
        public DelegateCommand OrderDeliveryPaidChanged { get; set; }
        public DelegateCommand OrderCarriedOutChanged { get; set; }
        public DelegateCommand PositionPaidChanged { get; set; }
        public DelegateCommand PositionCarriedOutChanged { get; set; }


        public DelegateCommand IncrementBagsInNewPosition { get; set; }
        public DelegateCommand DecrementBagsInNewPosition { get; set; }
        public DelegateCommand IncrementBagsInSelectedPosition { get; set; }
        public DelegateCommand DecrementBagsInSelectedPosition { get; set; }
        public DelegateCommand IncrementKgInNewPosition { get; set; }
        public DelegateCommand DecrementKgInNewPosition { get; set; }
        public DelegateCommand IncrementKgInSelectedPosition { get; set; }
        public DelegateCommand DecrementKgInSelectedPosition { get; set; }
        public DelegateCommand SelectedOrderChanged { get; set; }


        public Orders_ViewModel()
        {


            OpenWindowOfCreateOrder = new DelegateCommand(obj =>
            {
                NewDataForOrder = new Order()
                {
                    Date = DateTime.Now                    
                };
                NewDataForPositions = new ObservableCollection<PositionInOrder>();

                foreach(Warehouse w in AppModels.WarehouseModel.WarehouseItems)
                {
                    PositionInOrder NewPosition = new PositionInOrder()
                    {
                        Food = w.Food,
                        Order = NewDataForOrder
                    };
                    NewDataForPositions.Add(NewPosition);
                }


                SelectedTypeOfSale = TypeOfSales.FirstOrDefault();
                NewDataForOrder.TypeOfDelivery = TypeOfDeliveries.FirstOrDefault();
                NewDataForOrder.CostOfDelivery = 300;
                CreateOrder_Window window = new CreateOrder_Window();
                window.ShowDialog();
            });

            CreateOrder = new DelegateCommand(obj =>
            {
                if(NewDataForOrder.Client == null)
                {
                    MessageBox.Show("Выберите клиента!");
                    return;
                }
                if (!OneOrMorePositionIsNotNull())
                {
                    MessageBox.Show("Хотя бы одна позиция должна быть выбрана!");
                    return;
                }

                AppModels.OrdersModel.CreateOrder(NewDataForOrder);
                AppModels.OrdersModel.CreatePositionsfromList(NewDataForPositions.ToList());
                NewDataForOrder.RecalcAmount();
                (obj as Window).Close();
            });

            ClearOrder = new DelegateCommand(obj =>
            {
                if(SelectedOrder == null)
                {
                    MessageBox.Show("Выберите заказ!");
                    return;
                }
                if (AppModels.OrdersModel.CanClearOrder(SelectedOrder))
                {
                    AppModels.OrdersModel.ClearOrder(SelectedOrder);
                }
                else
                {
                    MessageBox.Show("Вы не можете аннулировать заказ, в котором есть оплаченные или проведенные позиции, " +
                        "а также если доставка оплачена!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            });

            IncrementBagsInNewPosition = new DelegateCommand(obj => {
                SelectedNewPosition.CountOfBags++;
            });
            DecrementBagsInNewPosition = new DelegateCommand(obj => {
                if (SelectedNewPosition.CountOfBags != 0)
                {
                    SelectedNewPosition.CountOfBags--;
                }
            });
            IncrementKgInNewPosition = new DelegateCommand(obj => {
                SelectedNewPosition.CountOfKG++;
            });
            DecrementKgInNewPosition = new DelegateCommand(obj => {
                if(SelectedNewPosition.CountOfKG != 0)
                {
                    SelectedNewPosition.CountOfKG--;
                }
            });


            IncrementBagsInSelectedPosition = new DelegateCommand(obj => {
                
            });
            DecrementBagsInSelectedPosition = new DelegateCommand(obj => {
                
            });
            IncrementKgInSelectedPosition = new DelegateCommand(obj => {
                
            });
            DecrementKgInSelectedPosition = new DelegateCommand(obj => {
                
            });
        }


        public void OnSelectedOrderChanged()
        {
            if(SelectedOrder != null)
            {
                PositionInOrders = new ObservableCollection<PositionInOrder>(SelectedOrder.PositionInOrder);
            }
            else
            {
                PositionInOrders = null;
            }
        }

        public void OnSelectedTypeOfSaleChanged()
        {
            foreach (PositionInOrder p in NewDataForPositions)
            {
                p.TypeOfSale = SelectedTypeOfSale;
            }
        }

        public void OnSelectedDiscountChanged()
        {
            foreach (PositionInOrder p in NewDataForPositions)
            {
                p.Discount = SelectedDiscount;
            }
        }


        /// <summary>
        /// Let checking positions on a value other that 0 when creating a new order
        /// </summary>
        /// <returns></returns>
        public bool OneOrMorePositionIsNotNull()
        {
            bool Result = false;
            foreach(PositionInOrder p in NewDataForPositions)
            {
                if(p.CountOfBags != 0 || p.CountOfKG != 0)
                {
                    Result = true;
                    break;
                }
            }
            return Result;
        }
    }
}
