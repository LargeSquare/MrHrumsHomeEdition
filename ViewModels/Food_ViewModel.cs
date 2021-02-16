using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using MrHrumsHomeEdition.Views.Client;
using MrHrumsHomeEdition.Views.Event;
using MrHrumsHomeEdition.Views.Food;
using MrHrumsHomeEdition.Views.Food.FoodName;
using MrHrumsHomeEdition.Views.Food.FoodWeight;
using MrHrumsHomeEdition.Views.Food.Granule;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class Food_ViewModel
    {
        public ObservableCollection<FoodName> FoodNames => AppModels.FoodModel.FoodNames;
        public ObservableCollection<FoodWeight> FoodWeights => AppModels.FoodModel.FoodWeights;
        public ObservableCollection<Granule> Granules => AppModels.FoodModel.Granules;
        public ObservableCollection<Price> Prices => AppModels.FoodModel.Prices;
        public ObservableCollection<Food> Foods => AppModels.FoodModel.Foods;


        public Food SelectedFood { get; set; }
        public FoodName SelectedFoodName { get; set; }
        public FoodWeight SelectedFoodWeight { get; set; }
        public Granule SelectedGranule { get; set; }


        // Local* using as container of new data for adding or changing selected item
        public Food LocalFood { get; set; }
        public FoodName LocalFoodName { get; set; }
        public FoodWeight LocalFoodWeight { get; set; }
        public Granule LocalGranule { get; set; }
        public Price LocalPrice { get; set; }


        public DelegateCommand OpenWindowOfCreateFoodName { get; set; }
        public DelegateCommand OpenWindowOfChangeFoodName { get; set; }
        public DelegateCommand CreateFoodName { get; set; }
        public DelegateCommand ChangeFoodName { get; set; }
        public DelegateCommand RemoveFoodName { get; set; }


        public DelegateCommand OpenWindowOfCreateFoodWeight { get; set; }
        public DelegateCommand OpenWindowOfChangeFoodWeight { get; set; }
        public DelegateCommand CreateFoodWeight { get; set; }
        public DelegateCommand ChangeFoodWeight { get; set; }
        public DelegateCommand RemoveFoodWeight { get; set; }


        public DelegateCommand OpenWindowOfCreateGranule { get; set; }
        public DelegateCommand OpenWindowOfChangeGranule { get; set; }
        public DelegateCommand CreateGranule { get; set; }
        public DelegateCommand ChangeGranule { get; set; }
        public DelegateCommand RemoveGranule { get; set; }


        public DelegateCommand OpenWindowOfCreateFood { get; set; }
        public DelegateCommand OpenWindowOfChangeFood { get; set; }
        public DelegateCommand CreateFood { get; set; }
        public DelegateCommand ChangeFood { get; set; }
        public DelegateCommand RemoveFood { get; set; }


        public Food_ViewModel()
        {
            OpenWindowOfCreateFoodName = new DelegateCommand(obj =>
            {
                LocalFoodName = new FoodName() { Visible = true };
                CreateFoodName_Window window = new CreateFoodName_Window();
                window.ShowDialog();
            });

            OpenWindowOfChangeFoodName = new DelegateCommand(obj =>
            {
                if(SelectedFoodName == null)
                {
                    MessageBox.Show("Выберите название!");
                    return;
                }
                LocalFoodName = new FoodName()
                {
                    Name = SelectedFoodName.Name,
                    Visible = true
                };
                ChangeFoodName_Window window = new ChangeFoodName_Window();
                window.ShowDialog();
            });

            CreateFoodName = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalFoodName.Name))
                {
                    MessageBox.Show("Введите название!");
                    return;
                }
                if (!AppModels.FoodModel.CanCreateFoodName(LocalFoodName))
                {
                    MessageBox.Show(
                        "Такое название уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.FoodModel.CreateFoodName(LocalFoodName);
                (obj as Window).Close();
            });

            ChangeFoodName = new DelegateCommand(obj =>
            {
                if(string.IsNullOrEmpty(LocalFoodName.Name))
                {
                    MessageBox.Show("Введите название!");
                    return;
                }
                if (!AppModels.FoodModel.CanCreateFoodName(LocalFoodName))
                {
                    MessageBox.Show(
                        "Такое название уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                AppModels.FoodModel.ChangeFoodName(SelectedFoodName, LocalFoodName);
                (obj as Window).Close();
            });

            RemoveFoodName = new DelegateCommand(obj =>
            {
                if (SelectedFoodName == null)
                {
                    MessageBox.Show("Выберите название!");
                    return;
                }
                if (!AppModels.FoodModel.CanRemoveFoodName(SelectedFoodName))
                {
                    MessageBox.Show(
                        "Корма с таким названием используются в прайсе!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.FoodModel.RemoveFoodName(SelectedFoodName);
            });




            OpenWindowOfCreateFoodWeight = new DelegateCommand(obj =>
            {
                LocalFoodWeight = new FoodWeight() { Visible = true };
                CreateFoodWeight_Window window = new CreateFoodWeight_Window();
                window.ShowDialog();
            });

            OpenWindowOfChangeFoodWeight = new DelegateCommand(obj =>
            {
                if (SelectedFoodWeight == null)
                {
                    MessageBox.Show("Выберите вес!");
                    return;
                }
                LocalFoodWeight = new FoodWeight()
                {
                    Weight = SelectedFoodWeight.Weight,
                    Visible = true
                };
                ChangeFoodWeight_Window window = new ChangeFoodWeight_Window();
                window.ShowDialog();
            });

            CreateFoodWeight = new DelegateCommand(obj =>
            {
                if (LocalFoodWeight.Weight <= 0)
                {
                    MessageBox.Show("Некорректный вес!");
                    return;
                }
                if (!AppModels.FoodModel.CanCreateFoodWeight(LocalFoodWeight))
                {
                    MessageBox.Show(
                        "Такой вес уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.FoodModel.CreateFoodWeight(LocalFoodWeight);
                (obj as Window).Close();
            });

            ChangeFoodWeight = new DelegateCommand(obj =>
            {
                if (LocalFoodWeight.Weight <= 0)
                {
                    MessageBox.Show("Некорректный вес!");
                    return;
                }
                if (!AppModels.FoodModel.CanCreateFoodWeight(LocalFoodWeight))
                {
                    MessageBox.Show(
                        "Такой вес уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                AppModels.FoodModel.ChangeFoodWeight(SelectedFoodWeight, LocalFoodWeight);
                (obj as Window).Close();
            });

            RemoveFoodWeight = new DelegateCommand(obj =>
            {
                if (SelectedFoodWeight == null)
                {
                    MessageBox.Show("Выберите вес!");
                    return;
                }
                if (!AppModels.FoodModel.CanRemoveFoodWeight(SelectedFoodWeight))
                {
                    MessageBox.Show(
                        "Корма с таким весом используются в прайсе!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.FoodModel.RemoveFoodWeight(SelectedFoodWeight);
            });




            OpenWindowOfCreateGranule = new DelegateCommand(obj =>
            {
                LocalGranule = new Granule() { Visible = true };
                CreateGranule_Window window = new CreateGranule_Window();
                window.ShowDialog();
            });

            OpenWindowOfChangeGranule = new DelegateCommand(obj =>
            {
                if (SelectedGranule == null)
                {
                    MessageBox.Show("Выберите гранулу!");
                    return;
                }
                LocalGranule = new Granule()
                {
                    Size = SelectedGranule.Size,
                    Visible = true
                };
                ChangeGranule_Window window = new ChangeGranule_Window();
                window.ShowDialog();
            });

            CreateGranule = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalGranule.Size))
                {
                    MessageBox.Show("Введите название гранулы!");
                    return;
                }
                if (!AppModels.FoodModel.CanCreateGranule(LocalGranule))
                {
                    MessageBox.Show(
                        "Такая гранула уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.FoodModel.CreateGranule(LocalGranule);
                (obj as Window).Close();
            });

            ChangeGranule = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalGranule.Size))
                {
                    MessageBox.Show("Введите название гранулы!");
                    return;
                }
                if (!AppModels.FoodModel.CanCreateGranule(LocalGranule))
                {
                    MessageBox.Show(
                        "Такая гранула уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                AppModels.FoodModel.ChangeGranule(SelectedGranule, LocalGranule);
                (obj as Window).Close();
            });

            RemoveGranule = new DelegateCommand(obj =>
            {
                if (SelectedGranule == null)
                {
                    MessageBox.Show("Выберите гранулу!");
                    return;
                }
                if (!AppModels.FoodModel.CanRemoveGranule(SelectedGranule))
                {
                    MessageBox.Show(
                        "Корма с такой гранулой используются в прайсе!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.FoodModel.RemoveGranule(SelectedGranule);
            });




            OpenWindowOfCreateFood = new DelegateCommand(obj =>
            {
                LocalFood = new Food() { Visible = true };
                LocalPrice = new Price() { Visible = true };
                LocalFood.Price = LocalPrice;

                CreateFood_Window window = new CreateFood_Window();
                window.ShowDialog();
            });

            OpenWindowOfChangeFood = new DelegateCommand(obj =>
            {
                if (SelectedFood == null)
                {
                    MessageBox.Show("Выберите корм!");
                    return;
                }

                LocalPrice = new Price() 
                {
                    Purchase = SelectedFood.Price.Purchase,
                    Kennel = SelectedFood.Price.Kennel,
                    Retail = SelectedFood.Price.Retail
                };

                LocalFood = new Food()
                {
                    FoodName = SelectedFood.FoodName,
                    FoodWeight = SelectedFood.FoodWeight,
                    Granule = SelectedFood.Granule,
                    Price = LocalPrice,
                    Visible = true                
                };
                ChangeFood_Window window = new ChangeFood_Window();
                window.ShowDialog();
            });

            CreateFood = new DelegateCommand(obj =>
            {
                // todo: make sure LocalFood has been created and filled when window of create food open
                if (LocalFood.FoodName == null)
                {
                    MessageBox.Show("Выберите название!");
                    return;
                }
                if (LocalFood.FoodWeight == null)
                {
                    MessageBox.Show("Выберите вес!");
                    return;
                }
                if (LocalFood.Granule == null)
                {
                    MessageBox.Show("Выберите гранулу!");
                    return;
                }
                if (LocalFood.Price.Purchase < 0)
                {
                    MessageBox.Show("Некорректная цена закупки!");
                    return;
                }
                if (LocalFood.Price.Retail < 0)
                {
                    MessageBox.Show("Некорректная розничная цена!");
                    return;
                }
                if (LocalFood.Price.Kennel < 0)
                {
                    MessageBox.Show("Некорректная цена для питомника!");
                    return;
                }



                if (!AppModels.FoodModel.CanCreateFood(LocalFood))
                {
                    MessageBox.Show(
                        "Такой корм уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!AppModels.WarehouseModel.CanCreateWarehouseItem(LocalFood))
                {
                    // todo: transit this method to FoodModel
                    MessageBox.Show(
                        "Такой корм на складе уже существует! " +
                        "Это критическая ошибка. Обратитесь к администратору", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.FoodModel.CreateFood(LocalFood);
                (obj as Window).Close();
            });

            ChangeFood = new DelegateCommand(obj =>
            {
                if (LocalFood.Price.Purchase < 0)
                {
                    MessageBox.Show("Некорректная цена закупки!");
                    return;
                }
                if (LocalFood.Price.Retail < 0)
                {
                    MessageBox.Show("Некорректная розничная цена!");
                    return;
                }
                if (LocalFood.Price.Kennel < 0)
                {
                    MessageBox.Show("Некорректная цена для питомника!");
                    return;
                }

                if (!AppModels.FoodModel.CanCreateFood(LocalFood))
                {
                    MessageBox.Show(
                        "Такой корм уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.FoodModel.ChangeFood(SelectedFood, LocalFood);
                (obj as Window).Close();
            });

            RemoveFood = new DelegateCommand(obj =>
            {
                if (SelectedFood == null)
                {
                    MessageBox.Show("Выберите корм!");
                    return;
                }
                if (!AppModels.FoodModel.CanRemoveFood(SelectedFood))
                {
                    MessageBox.Show(
                        "На складе еще есть мешки удаляемого корма!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.FoodModel.RemoveFood(SelectedFood);
            });
        }

    }
}
