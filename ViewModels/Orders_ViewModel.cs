using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using MrHrumsHomeEdition.Views.Client;
using MrHrumsHomeEdition.Views.Event;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows;
using MrHrumsHomeEdition.Data.DataBaseModels;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class Orders_ViewModel
    {
        public ObservableCollection<Order> Orders => AppModels.OrdersModel.Orders;
        public ObservableCollection<PositionInOrder> PositionInOrders { get; set; }
        public ObservableCollection<TypeOfSale> TypeOfSales => AppModels.OrdersModel.TypeOfSales;
        public ObservableCollection<TypeOfDelivery> TypeOfDeliveries => AppModels.OrdersModel.TypeOfDeliveries;


        public Order NewDataForOrder { get; set; }
        public ObservableCollection<PositionInOrder> NewDataForPositions { get; set; }


        public Order SelectedOrder { get; set; }
        public PositionInOrder SelectedPositionInOrder { get; set; }
        public PositionInOrder SelectedNewPosition { get; set; }
        public TypeOfSale SelectedTypeOfSale { get; set; }
        public Client SelectedClient { get; set; }


        //commands
        public DelegateCommand OpenWindowOfCreateOrder { get; set; }
        public DelegateCommand OpenWindowOfCreatePositions { get; set; }
        public DelegateCommand OpenWindowOfChangePosition { get; set; }
        public DelegateCommand CreateOrder { get; set; }
        public DelegateCommand ClearOrder { get; set; }
        public DelegateCommand CreatePositions { get; set; }
        public DelegateCommand ChangePosition { get; set; }
        public DelegateCommand ClearPosition { get; set; }


        public DelegateCommand OrderPaidChanged { get; set; }
        public DelegateCommand OrderDeliveryPaidChanged { get; set; }
        public DelegateCommand OrderCarriedOutChanged { get; set; }
        public DelegateCommand PositionPaidChanged { get; set; }
        public DelegateCommand PositionCarriedOutChanged { get; set; }


        public DelegateCommand IncrementPosition { get; set; }
        public DelegateCommand DecrementPosition { get; set; }
        public DelegateCommand IncrementSelectedPosition { get; set; }
        public DelegateCommand DecrementSelectedPosition { get; set; }
        public DelegateCommand SelectedOrderChanged { get; set; }


        public Orders_ViewModel()
        {

        }


        private void UpdatePositionInOrder()
        {
            PositionInOrders = new ObservableCollection<PositionInOrder>(SelectedOrder.PositionInOrder);
        }
    }
}
