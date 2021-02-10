using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using MrHrumsHomeEdition.Views.Event;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows;
using MrHrumsModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class Events_ViewModel
    {
        public ObservableCollection<Event> Events => MrHrumsModels.EventsModel.Events;
        public ObservableCollection<TypeOfEvent> TypesOfEvent => MrHrumsModels.EventsModel.TypesOfEvent;

        public TypeOfEvent SelectedTypeOfEvent { get; set; }

        // LocalTypeOfEvent using as container of new data for adding or changing selected type
        public TypeOfEvent LocalTypeOfEvent { get; set; }

        public DelegateCommand OpenWindowOfAddTypeOfEvent { get; set; }
        public DelegateCommand OpenWindowOfChangeTypeOfEvent { get; set; }
        public DelegateCommand AddTypeOfEvent { get; set; }
        public DelegateCommand ChangeTypeOfEvent { get; set; }
        public DelegateCommand RemoveTypeOfEvent { get; set; }


        public Events_ViewModel()
        {
            OpenWindowOfAddTypeOfEvent = new DelegateCommand(obj =>
            {
                LocalTypeOfEvent = new TypeOfEvent()
                {
                    Visible = true,
                    IsSystem = false
                };
                AddTypeOfEvent_Window window = new AddTypeOfEvent_Window();
                window.ShowDialog();
            });

            OpenWindowOfChangeTypeOfEvent = new DelegateCommand(obj =>
            {
                if (SelectedTypeOfEvent == null)
                {
                    MessageBox.Show("Выберите тип события!");
                    return;
                }
                if (SelectedTypeOfEvent.IsSystem == true)
                {
                    MessageBox.Show(
                    "Ошибка!",
                    "Вы не можете изменить системный тип события",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    return;
                }

                LocalTypeOfEvent = new TypeOfEvent()
                {
                    Type = SelectedTypeOfEvent.Type,
                    Visible = true,
                    IsSystem = false
                };

                ChangeTypeOfEvent_Window window = new ChangeTypeOfEvent_Window();
                window.ShowDialog();
            });

            AddTypeOfEvent = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalTypeOfEvent.Type))
                {
                    MessageBox.Show("Введите название типа!");
                    return;
                }

                MrHrumsModels.EventsModel.AddTypeOfEvent(LocalTypeOfEvent);
                (obj as Window).Close();
            });

            ChangeTypeOfEvent = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalTypeOfEvent.Type))
                {
                    MessageBox.Show("Введите название типа!");
                    return;
                }

                MrHrumsModels.EventsModel.ChangeTypeOfEvent(SelectedTypeOfEvent, LocalTypeOfEvent);
                (obj as Window).Close();
            });

            RemoveTypeOfEvent = new DelegateCommand(obj =>
            {
                if(SelectedTypeOfEvent == null)
                {
                    MessageBox.Show("Выберите тип события!");
                    return;
                }
                if (SelectedTypeOfEvent.IsSystem == true)
                {
                    MessageBox.Show(
                    "Ошибка!",
                    "Вы не можете изменить системный тип события",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    return;
                }

                MrHrumsModels.EventsModel.RemoveTypeOfEvent(SelectedTypeOfEvent);
            });

        }
    }
}
