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
    class Clients_Model
    {
        public ObservableCollection<Client> Clients { get; set; }


        public Clients_Model()
        {
            DB.Client.Load();

            Clients = DB.Client.Local;
        }

        public void AddClient(Client client)
        {
            Clients.Add(client);
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 9),
                Date = DateTime.Now,
                Message = string.Format("Создание клиента: {0} ",
                                        client.Name)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }

        public void ChangeClient(Client OldClient, Client NewClient)
        {
            Client item = Clients.FirstOrDefault(c => c.Id == OldClient.Id);
            if (item == null)
            {
                MessageBox.Show(
                    "Невозможно редактировать клиента! Обратитесь к администратору.", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            item.Name = NewClient.Name;
            item.Address = NewClient.Address;
            item.Number = NewClient.Number;
            item.Note = NewClient.Note;

            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 10),
                Date = DateTime.Now,
                Message = string.Format("Редактирование клиента: {0} -> {1}, {2} -> {3}, {4} -> {5}",
                                        OldClient.Name, NewClient.Name,
                                        OldClient.Address, NewClient.Address,
                                        OldClient.Number, NewClient.Number)
            };
            AppModels.EventsModel.AddEvent(NewEvent);

            DB.SaveChanges();
        }

        public void RemoveClient(Client client)
        {
            client.Visible = false;
            Event NewEvent = new Event()
            {
                TypeOfEvent = AppModels.EventsModel.TypesOfEvent.FirstOrDefault(t => t.Id == 11),
                Date = DateTime.Now,
                Message = string.Format("Удаление клиента: {0}",
                                        client.Name)
            };
            AppModels.EventsModel.AddEvent(NewEvent);
            DB.SaveChanges();
        }
    }
}
