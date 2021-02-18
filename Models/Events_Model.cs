using MrHrumsHomeEdition.Data;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using static MrHrumsHomeEdition.OtherClasses.DataBaseConnection;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.Models
{
    [AddINotifyPropertyChangedInterface]
    class Events_Model
    {
        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<TypeOfEvent> TypesOfEvent { get; set; }

        public Events_Model()
        {
            DB.Event.Load();
            DB.TypeOfEvent.Load();

            Events = DB.Event.Local;
            TypesOfEvent = DB.TypeOfEvent.Local;
        }


        public void AddEvent(Event e)
        {
            Events.Add(e);
            DB.SaveChanges();
        }

        public void ClearEvents()
        {
            Events.Clear();
            DB.SaveChanges();
        }

        public void AddTypeOfEvent(TypeOfEvent Type)
        {
            TypesOfEvent.Add(Type);
            Event NewEvent = new Event()
            {
                TypeOfEvent = TypesOfEvent.FirstOrDefault(t => t.Id == 1),
                Date = DateTime.Now,
                Message = string.Format("Создание нового типа события: {0} ",
                                        Type.Type)
            };
            AddEvent(NewEvent);
            DB.SaveChanges();
        }

        public void ChangeTypeOfEvent(TypeOfEvent OldType, TypeOfEvent NewType)
        {
            if (OldType.IsSystem)
            {
                MessageBox.Show(
                    "Ошибка!", "Вы не можете изменить системный тип события",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TypeOfEvent item = TypesOfEvent.FirstOrDefault( t=> t.Id == OldType.Id);
            if(item == null)
            {
                MessageBox.Show(
                    "Невозможно изменить тип события! Обратитесь к администратору.", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            item.Type = NewType.Type;
            Event NewEvent = new Event()
            {
                TypeOfEvent = TypesOfEvent.FirstOrDefault(t => t.Id == 2),
                Date = DateTime.Now,
                Message = string.Format("Редактирование типа события: {0} -> {1} ",
                                        OldType.Type,
                                        NewType.Type)
            };
            AddEvent(NewEvent);
            DB.SaveChanges();
        }

        public void RemoveTypeOfEvent(TypeOfEvent Type)
        {
            if (Type.IsSystem)
            {
                MessageBox.Show(
                    "Вы не можете удалить системный тип события", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Type.Visible = false;
            Event NewEvent = new Event()
            {
                TypeOfEvent = TypesOfEvent.FirstOrDefault(t => t.Id == 3),
                Date = DateTime.Now,
                Message = string.Format("Удаление типа события: {0} ",
                                        Type.Type)
            };
            AddEvent(NewEvent);
            DB.SaveChanges();
        }

        public bool CanCreateTypeOfEvent(TypeOfEvent Type)
        {
            TypeOfEvent CheckableItem = TypesOfEvent.FirstOrDefault(t =>
                                               t.Type == Type.Type);
            bool Result = CheckableItem == null ? true : false;
            return Result;
        }
    }
}
