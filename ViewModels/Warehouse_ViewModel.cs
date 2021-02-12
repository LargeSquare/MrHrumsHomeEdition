using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using MrHrumsHomeEdition.Views.Client;
using MrHrumsHomeEdition.Views.Event;
using MrHrumsHomeEdition.Views.Warehouse;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows;
using MrHrumsModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class Warehouse_ViewModel
    {
        public ObservableCollection<Warehouse> WarehouseItems => MrHrumsModels.WarehouseModel.WarehouseItems;

        public Warehouse SelectedWarehouse { get; set; }
        public int countOfBags;
        public int CountOfBags {
            get
            {
                return countOfBags;
            }
            set
            {
                if (value >= 0)
                {
                    countOfBags = value;
                }
            }
        }

        public DelegateCommand OpenWindowOfPackingItem { get; set; }
        public DelegateCommand OpenWindowOfUnpackingItem { get; set; }
        public DelegateCommand PackingItem { get; set; }
        public DelegateCommand UnpackingItem { get; set; }


        public Warehouse_ViewModel()
        {
            OpenWindowOfPackingItem = new DelegateCommand(obj =>
            {
                if(SelectedWarehouse == null)
                {
                    MessageBox.Show("Выберите позицию!");
                    return;
                }
                if(SelectedWarehouse.KG.Count < SelectedWarehouse.Food.FoodWeight.Weight)
                {
                    MessageBox.Show("На складе недостаточно КГ выбранного корма!");
                    return;
                }
                CountOfBags = 0;

                PackingWarehouseItem_Window window = new PackingWarehouseItem_Window();
                window.ShowDialog();
            });

            OpenWindowOfUnpackingItem = new DelegateCommand(obj =>
            {
                if (SelectedWarehouse == null)
                {
                    MessageBox.Show("Выберите позицию!");
                    return;
                }
                if(SelectedWarehouse.Bags.Count == 0)
                {
                    MessageBox.Show("На складе недостаточно мешков выбранного корма!");
                    return;
                }
                CountOfBags = 0;

                UnpackingWarehouseItem_Window window = new UnpackingWarehouseItem_Window();
                window.ShowDialog();
            });

            PackingItem = new DelegateCommand(obj =>
            {
                if (MrHrumsModels.WarehouseModel.CanPackingItem(SelectedWarehouse, CountOfBags))
                {
                    MrHrumsModels.WarehouseModel.PackingItem(SelectedWarehouse, CountOfBags);
                    (obj as Window).Close();
                }
                else
                {
                    MessageBox.Show(
                        "Ошибка",
                        "Ошибка создания мешка(ов)!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            });

            UnpackingItem = new DelegateCommand(obj =>
            {
                if (MrHrumsModels.WarehouseModel.CanUnpackingItem(SelectedWarehouse, CountOfBags))
                {
                    MrHrumsModels.WarehouseModel.UnpackingItem(SelectedWarehouse, CountOfBags);
                    (obj as Window).Close();
                }
                else
                {
                    MessageBox.Show(
                        "Ошибка",
                        "Ошибка распаковки мешка(ов)!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            });
        }
    }
}
