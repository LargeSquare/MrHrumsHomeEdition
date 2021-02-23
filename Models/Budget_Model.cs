using MrHrumsHomeEdition.Data;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using MrHrumsHomeEdition.Data.DataBaseModels;
using static MrHrumsHomeEdition.OtherClasses.DataBaseConnection;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;

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
            DB.Budgets.Load();
            DB.BudgetStates.Load();
            DB.TypeOfBudgetActions.Load();

            BudgetItems = DB.Budgets.Local;
            BudgetStates = DB.BudgetStates.Local;
            TypesOfBudgetAction = DB.TypeOfBudgetActions.Local;
        }

        public void AddBudgetItem(Budget BudgetItem)
        {
            BudgetItems.Add(BudgetItem);
            string EventMessage = "";
            int index = 0;
            switch (BudgetItem.BudgetState.TypeOfBudgetAction.Id)
            {
                case 0:
                    index = 4;
                    EventMessage = "Добавление дохода: {0}р.";
                    break;
                case 1:
                    index = 5;
                    EventMessage = "Добавление расхода: {0}р.";
                    break;
            }

            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == index),
                Date = DateTime.Now,
                Message = string.Format(EventMessage,
                                        BudgetItem.Amount)
            };
            AppModels.EventsModel.AddEvent(NewEvent);

            DB.SaveChanges();
        }

        public void AddBudgetState(BudgetState BudgetStateItem)
        {
            BudgetStates.Add(BudgetStateItem);
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 6),
                Date = DateTime.Now,
                Message = string.Format("Создание новой статьи бюджет: {0} ",
                                        BudgetStateItem.Name)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
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
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 7),
                Date = DateTime.Now,
                Message = string.Format("Редактирование статьи бюджета: {0} -> {1}",
                                        OldBudgetStateItem.Name,
                                        NewBudgetStateItem.Name)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
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
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 8),
                Date = DateTime.Now,
                Message = string.Format("Удаление статьи бюджет: {0} ",
                                        BudgetStateItem.Name)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
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
