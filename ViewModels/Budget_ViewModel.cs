using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using MrHrumsHomeEdition.Views.Budget;
using MrHrumsHomeEdition.Views.Event;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using MrHrumsModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class Budget_ViewModel
    {
        public ObservableCollection<Budget> BudgetItems => MrHrumsModels.BudgetModel.BudgetItems;
        public ObservableCollection<BudgetState> BudgetStates => MrHrumsModels.BudgetModel.BudgetStates;
        public ObservableCollection<TypeOfBudgetAction> TypesOfBudgetAction => MrHrumsModels.BudgetModel.TypesOfBudgetAction;


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
                    "Ошибка!",
                    "Вы не можете изменить системную статью бюджета !",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    return;
                }

                LocalBudgetState = SelectedBudgetState;

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

                MrHrumsModels.BudgetModel.AddBudgetItem(LocalBudgetItem);
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
                MrHrumsModels.BudgetModel.AddBudgetState(LocalBudgetState);
                (obj as Window).Close();
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
                MrHrumsModels.BudgetModel.ChangeBudgetState(SelectedBudgetState, LocalBudgetState);
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
                    "Ошибка!",
                    "Вы не можете удалить системную статью бюджета!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    return;
                }
                MrHrumsModels.BudgetModel.RemoveBudgetState(SelectedBudgetState);
            });
        }
    }
}
