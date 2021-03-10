using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using MrHrumsHomeEdition.Views.Budget;
using MrHrumsHomeEdition.Views.Event;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using MrHrumsHomeEdition.Data.DataBaseModels;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.ViewModels
{
    class Budget_ViewModel : BasePropertyChanged
    {
        public ObservableCollection<Budget> BudgetItems => AppModels.BudgetModel.BudgetItems;
        public ObservableCollection<BudgetState> BudgetStates => AppModels.BudgetModel.BudgetStates;
        public ObservableCollection<TypeOfBudgetAction> TypesOfBudgetAction => AppModels.BudgetModel.TypesOfBudgetAction;


        public Budget LocalBudgetItem { get; set; }
        public BudgetState SelectedBudgetState { get; set; }
        public BudgetState LocalBudgetState { get; set; }


        public DelegateCommand OpenWindowOfAddBudgetItem { get; set; }
        public DelegateCommand OpenWindowOfAddBudgetState { get; set; }
        public DelegateCommand OpenWindowOfChangeBudgetState { get; set; }
        public DelegateCommand AddBudgetItem { get; set; }
        public DelegateCommand AddBudgetState { get; set; }
        public DelegateCommand ChangeBudgetState { get; set; }
        public DelegateCommand RemoveBudgetState { get; set; }


        public Budget_ViewModel()
        {
            OpenWindowOfAddBudgetItem = new DelegateCommand(obj =>
            {
                LocalBudgetItem = new Budget();

                AddBudgetItem_Window window = new AddBudgetItem_Window();
                window.ShowDialog();
            });

            OpenWindowOfAddBudgetState = new DelegateCommand(obj =>
            {
                LocalBudgetState = new BudgetState() {
                    IsSystem = false,
                    Visible = true
                };

                AddBudgetState_Window window = new AddBudgetState_Window();
                window.ShowDialog();
            });

            OpenWindowOfChangeBudgetState = new DelegateCommand(obj =>
            {
                if (SelectedBudgetState == null)
                {
                    MessageBox.Show("Выберите статью!");
                    return;
                }
                if (SelectedBudgetState.IsSystem)
                {
                    MessageBox.Show(
                    "Вы не можете изменить системную статью бюджета !", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                LocalBudgetState = new BudgetState()
                {
                    Name = SelectedBudgetState.Name,
                    TypeOfBudgetAction = SelectedBudgetState.TypeOfBudgetAction,
                    Visible = SelectedBudgetState.Visible
                };

                ChangeBudgetState_Window window = new ChangeBudgetState_Window();
                window.ShowDialog();
            });

            AddBudgetItem = new DelegateCommand(obj =>
            {
                if (LocalBudgetItem.Amount == 0)
                {
                    MessageBox.Show("Введите сумму операции!");
                    return;
                }
                if (LocalBudgetItem.BudgetState == null)
                {
                    MessageBox.Show("Выберите статью!");
                    return;
                }

                LocalBudgetItem.Date = DateTime.Now;

                AppModels.BudgetModel.AddBudgetItem(LocalBudgetItem);
                (obj as Window).Close();
            });

            AddBudgetState = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalBudgetState.Name))
                {
                    MessageBox.Show("Введите название!");
                    return;
                }
                if(LocalBudgetState.TypeOfBudgetAction == null)
                {
                    MessageBox.Show("Выберите тип операции!");
                    return;
                }

                if (AppModels.BudgetModel.CanCreateBudgetState(LocalBudgetState))
                {
                    AppModels.BudgetModel.AddBudgetState(LocalBudgetState);
                    (obj as Window).Close();
                }
                else
                {
                    MessageBox.Show("Такая статья уже существует!");
                }
            });

            ChangeBudgetState = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalBudgetState.Name))
                {
                    MessageBox.Show("Введите название!");
                    return;
                }
                if (LocalBudgetState.TypeOfBudgetAction == null)
                {
                    MessageBox.Show("Выберите тип операции!");
                    return;
                }
                AppModels.BudgetModel.ChangeBudgetState(SelectedBudgetState, LocalBudgetState);
                (obj as Window).Close();
            });

            RemoveBudgetState = new DelegateCommand(obj =>
            {
                if(SelectedBudgetState == null)
                {
                    MessageBox.Show("Выберите статью!");
                    return;
                }
                if (SelectedBudgetState.IsSystem)
                {
                    MessageBox.Show(
                        "Вы не можете удалить системную статью бюджета!", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                AppModels.BudgetModel.RemoveBudgetState(SelectedBudgetState);
            });
        }
    }
}
