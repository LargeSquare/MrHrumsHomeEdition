using MrHrumsHomeEdition.Data;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using static MrHrumsHomeEdition.OtherClasses.DataBaseConnection;

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
            DB.SaveChanges();
        }
    }
}
