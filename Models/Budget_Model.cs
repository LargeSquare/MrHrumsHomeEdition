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
    class Budget_Model
    {
        public ObservableCollection<Budget> BudgetItems { get; set; }
        public ObservableCollection<BudgetState> BudgetStates { get; set; }
        public ObservableCollection<TypeOfBudgetAction> TypesOfBudgetAction { get; set; }


        public Budget_Model()
        {
            DB.Budget.Load();
            DB.BudgetState.Load();
            DB.TypeOfBudgetAction.Load();

            BudgetItems = DB.Budget.Local;
            BudgetStates = DB.BudgetState.Local;
            TypesOfBudgetAction = DB.TypeOfBudgetAction.Local;
        }

        public void AddBudgetItem(Budget BudgetItem)
        {
            BudgetItems.Add(BudgetItem);
            DB.SaveChanges();
        }

        public void AddBudgetState(BudgetState BudgetStateItem)
        {
            BudgetStates.Add(BudgetStateItem);
            DB.SaveChanges();
        }

        public void ChangeBudgetState(BudgetState OldBudgetStateItem, BudgetState NewBudgetStateItem)
        {
            if (OldBudgetStateItem.IsSystem)
            {
                MessageBox.Show(
                    "Вы не можете изменить системную статью бюджета!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            BudgetState item = BudgetStates.FirstOrDefault(b => b.Id == OldBudgetStateItem.Id);
            if (item == null)
            {
                MessageBox.Show(
                    "Невозможно изменить статью! Обратитесь к администратору.", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            item.Name = NewBudgetStateItem.Name;
            DB.SaveChanges();
        }

        public void RemoveBudgetState(BudgetState BudgetStateItem)
        {
            if (BudgetStateItem.IsSystem)
            {
                MessageBox.Show(
                    "Вы не можете удалить системную статью бюджета!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BudgetStateItem.Visible = false;
            DB.SaveChanges();
        }

        public bool CanCreateBudgetState(BudgetState BudgetStateItem)
        {
            BudgetState CheckableItem = BudgetStates.FirstOrDefault(b =>
                                               b.Name == BudgetStateItem.Name &&
                                               b.Visible == true);
            bool Result = CheckableItem == null ? true : false;
            return Result;
        }
    }
}
