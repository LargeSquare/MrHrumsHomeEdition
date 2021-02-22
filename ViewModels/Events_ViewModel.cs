using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using MrHrumsHomeEdition.Views.Event;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows;
using MrHrumsHomeEdition.Data.DataBaseModels;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class Events_ViewModel
    {
        public ObservableCollection<Event> Events => AppModels.EventsModel.Events;
        public ObservableCollection<TypeOfEvent> TypesOfEvent => AppModels.EventsModel.TypesOfEvent;

        public TypeOfEvent SelectedTypeOfEvent { get; set; }

        // LocalTypeOfEvent using as container of new data for adding or changing selected type
        public TypeOfEvent LocalTypeOfEvent { get; set; }

        public DelegateCommand OpenWindowOfAddTypeOfEvent { get; set; }
        public DelegateCommand OpenWindowOfChangeTypeOfEvent { get; set; }
        public DelegateCommand ClearEvents { get; set; }
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
                        "Вы не можете изменить системный тип события", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
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

            ClearEvents = new DelegateCommand(obj =>
            {
                MessageBoxResult result =
                MessageBox.Show(
                    "Вы точно хотите очистить историю событий?", "Вопрос",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    AppModels.EventsModel.ClearEvents();
                }
            });

            AddTypeOfEvent = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalTypeOfEvent.Type))
                {
                    MessageBox.Show("Введите название типа!");
                    return;
                }

                if (!AppModels.EventsModel.CanCreateTypeOfEvent(LocalTypeOfEvent))
                {
                    MessageBox.Show(
                        "Такой тип события уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                AppModels.EventsModel.AddTypeOfEvent(LocalTypeOfEvent);
                (obj as Window).Close();
            });

            ChangeTypeOfEvent = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalTypeOfEvent.Type))
                {
                    MessageBox.Show("Введите название типа!");
                    return;
                }
                if (!AppModels.EventsModel.CanCreateTypeOfEvent(LocalTypeOfEvent))
                {
                    MessageBox.Show(
                        "Такой тип события уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                AppModels.EventsModel.ChangeTypeOfEvent(SelectedTypeOfEvent, LocalTypeOfEvent);
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
                        "Вы не можете удалить системный тип события", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AppModels.EventsModel.RemoveTypeOfEvent(SelectedTypeOfEvent);
            });

        }
    }
}
