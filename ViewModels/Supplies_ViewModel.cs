using MrHrumsHomeEdition.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PropertyChanged;
using System.Collections.ObjectModel;
using MrHrumsHomeEdition.Data.DataBaseModels;
using System.Windows.Controls;
using MrHrumsHomeEdition.Views.Supply;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class Supplies_ViewModel
    {
        public ObservableCollection<Supply> Supplies => AppModels.SuppliesModel.Supplies;
        public ObservableCollection<PositionInSupply> PositionInSupplies { get; set; }

        public Supply NewDataForSupply { get; set; }
        public ObservableCollection<PositionInSupply> NewDataForPositions { get; set; }

        public Supply SelectedSupply { get; set; }
        public PositionInSupply SelectedPositionInSupply { get; set; }
        public PositionInSupply SelectedNewPosition { get; set; }


        //commands
        public DelegateCommand OpenWindowOfCreateSupply { get; set; }
        public DelegateCommand OpenWindowOfCreatePositions { get; set; }
        public DelegateCommand OpenWindowOfChangePosition { get; set; }
        public DelegateCommand CreateSupply { get; set; }
        public DelegateCommand ClearSupply { get; set; }
        public DelegateCommand CreatePositions { get; set; }
        public DelegateCommand ChangePosition { get; set; }
        public DelegateCommand ClearPosition { get; set; }


        public DelegateCommand SupplyPaidChanged { get; set; }
        public DelegateCommand SupplyCarriedOutChanged { get; set; }
        public DelegateCommand PositionPaidChanged { get; set; }
        public DelegateCommand PositionCarriedOutChanged { get; set; }


        public DelegateCommand IncrementPosition { get; set; }
        public DelegateCommand DecrementPosition { get; set; }
        public DelegateCommand IncrementSelectedPosition { get; set; }
        public DelegateCommand DecrementSelectedPosition { get; set; }
        public DelegateCommand SelectedSupplyChanged { get; set; }

        public Supplies_ViewModel()
        {
            OpenWindowOfCreateSupply = new DelegateCommand(obj =>
            {
                NewDataForSupply = new Supply();
                NewDataForPositions = new ObservableCollection<PositionInSupply>();

                List<Food> foods = AppModels.FoodModel.Foods.OrderBy(f => f.FoodName.Name).ToList();
                foreach (Food f in foods)
                {
                    if (f.Visible == true)
                    {
                        PositionInSupply p = new PositionInSupply()
                        {
                            Food = f,
                            Supply = NewDataForSupply
                        };
                        NewDataForPositions.Add(p);
                    }
                }

                CreateSupply_Window window = new CreateSupply_Window();
                window.ShowDialog();
            });

            OpenWindowOfCreatePositions = new DelegateCommand(obj =>
            {
                if(SelectedSupply == null)
                {
                    MessageBox.Show("Выберите поставку!");
                    return;
                }

                if (AppModels.SuppliesModel.CanCreatePositionInSupply(SelectedSupply))
                {
                    NewDataForPositions = new ObservableCollection<PositionInSupply>();

                    List<Food> foods = AppModels.FoodModel.Foods.OrderBy(f => f.FoodName.Name).ToList();
                    foreach (Food f in foods)
                    {
                        if (f.Visible == true)
                        {
                            PositionInSupply p = new PositionInSupply()
                            {
                                Food = f,
                                Supply = SelectedSupply
                            };
                            NewDataForPositions.Add(p);
                        }
                    }

                    CreatePositionsInSupply_Window window = new CreatePositionsInSupply_Window();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Вы не можете добавить позиции в проведенную поставку", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            });

            OpenWindowOfChangePosition = new DelegateCommand(obj =>
            {
                if(SelectedPositionInSupply == null)
                {
                    MessageBox.Show("Выберите позицию!");
                    return;
                }

                if (AppModels.SuppliesModel.CanChangePosition(SelectedPositionInSupply))
                {
                    ChangePositionInSupply_Window window = new ChangePositionInSupply_Window();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Вы не можете изменить проведенную или оплаченную поставку", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            });

            CreateSupply = new DelegateCommand(obj =>
            {
                //check that at least one position have not null bags
                bool PositionNotNull = false;
                foreach(PositionInSupply p in NewDataForPositions)
                {
                    if(p.CountOfBags != 0)
                    {
                        PositionNotNull = true;
                    }
                }

                if(!PositionNotNull)
                {
                    MessageBox.Show("Хотябы одна позиция должна быть заполнена!");
                    return;
                }


                //main part of code
                AppModels.SuppliesModel.CreateSupply(NewDataForSupply);
                AppModels.SuppliesModel.CreatePositionsfromList(NewDataForPositions.ToList());
                AppModels.SuppliesModel.RecalcAmountOfSupply(NewDataForSupply);
                (obj as Window).Close();
            });
            ClearSupply = new DelegateCommand(obj =>
            {
                if (AppModels.SuppliesModel.CanClearSupply(SelectedSupply))
                {
                    AppModels.SuppliesModel.ClearSupply(SelectedSupply);
                }
                else
                {
                    MessageBox.Show("Вы не можете аннулировать поставку, в которой есть оплаченные или проведенные позиции", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            });

            CreatePositions = new DelegateCommand(obj =>
            {
                //check that at least one position have not null bags
                bool PositionNotNull = false;
                foreach (PositionInSupply p in NewDataForPositions)
                {
                    if (p.CountOfBags != 0)
                    {
                        PositionNotNull = true;
                    }
                }

                if (!PositionNotNull)
                {
                    MessageBox.Show("Хотябы одна позиция должна быть заполнена!");
                    return;
                }


                //main part of code
                AppModels.SuppliesModel.UpdateSupply(SelectedSupply, NewDataForPositions.ToList());
                AppModels.SuppliesModel.RecalcAmountOfSupply(SelectedSupply);
                UpdatePositionInSupplies();
                (obj as Window).Close();
            });
            ChangePosition = new DelegateCommand(obj =>
            {
                if(SelectedPositionInSupply.CountOfBags == 0)
                {
                    MessageBox.Show("Мешков не может быть меньше 1!");
                    return;
                }

                AppModels.SuppliesModel.ChangePosition(SelectedPositionInSupply);
                AppModels.SuppliesModel.RecalcAmountOfSupply(SelectedSupply);
                (obj as Window).Close();
            });
            ClearPosition = new DelegateCommand(obj =>
            {
                if(SelectedPositionInSupply == null)
                {
                    MessageBox.Show("Выберите позицию!");
                    return;
                }

                if(AppModels.SuppliesModel.CanClearPositionInSupply(SelectedPositionInSupply))
                {
                    AppModels.SuppliesModel.ClearPosition(SelectedPositionInSupply);
                    AppModels.SuppliesModel.RecalcAmountOfSupply(SelectedSupply);
                    UpdatePositionInSupplies();
                }
                else
                {
                    MessageBox.Show("Вы не можете аннулировать проведенную или оплаченную позицию", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            });

            SupplyPaidChanged = new DelegateCommand(obj =>
            {
                AppModels.SuppliesModel.SetPaidForSupply(SelectedSupply);
            });
            SupplyCarriedOutChanged = new DelegateCommand(obj =>
            {
                if (AppModels.SuppliesModel.CanSetCarriedOutForSupply(SelectedSupply))
                {
                    AppModels.SuppliesModel.SetCarriedOutForSupply(SelectedSupply);
                }
                else
                {
                    MessageBox.Show("Ноу");
                    (obj as CheckBox).IsChecked = !(obj as CheckBox).IsChecked;
                    return;
                }
            });
            PositionPaidChanged = new DelegateCommand(obj =>
            {
                AppModels.SuppliesModel.SetPaidForPosition(SelectedPositionInSupply);
            });
            PositionCarriedOutChanged = new DelegateCommand(obj =>
            {
                if (AppModels.SuppliesModel.CanSetCarriedOutForPosition(SelectedPositionInSupply))
                {
                    AppModels.SuppliesModel.SetCarriedOutForPosition(SelectedPositionInSupply);
                }
                else
                {
                    MessageBox.Show("Вы не можете провести поставку позиции, т.к." +
                        " такая позиция отсутствует на складе или поставка проведена!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    (obj as CheckBox).IsChecked = !(obj as CheckBox).IsChecked;
                    return;
                }
            });

            IncrementPosition = new DelegateCommand(obj =>
            {
                SelectedNewPosition.CountOfBags++;
            });
            DecrementPosition = new DelegateCommand(obj =>
            {
                if(SelectedNewPosition.CountOfBags != 0)
                {
                    SelectedNewPosition.CountOfBags--;
                }
            });
            IncrementSelectedPosition = new DelegateCommand(obj =>
            {
                SelectedPositionInSupply.CountOfBags++;
            });
            DecrementSelectedPosition = new DelegateCommand(obj =>
            {
                SelectedPositionInSupply.CountOfBags--;
            });
            SelectedSupplyChanged = new DelegateCommand(obj =>
            {
                if(SelectedSupply != null)
                {
                    UpdatePositionInSupplies();
                }
                else
                {
                    PositionInSupplies = null;
                }
            });
        }



        private void UpdatePositionInSupplies()
        {
            PositionInSupplies = new ObservableCollection<PositionInSupply>(SelectedSupply.PositionInSupply);
        }
    }
}
